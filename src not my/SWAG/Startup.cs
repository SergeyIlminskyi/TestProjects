using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Abstractions.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SWAG.Data;
using SWAG.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWAG
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Env { get; private set; }

        protected Boolean IsEntityFrameworkAction { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;

            String[] args = Environment.GetCommandLineArgs();
            if ((args?.Length ?? 0) > 0)
            {
                Int32 index = Array.IndexOf(args, "database");

                if (index >= 0 && args.Length > index)
                {
                    if ((args.ElementAt(index + 1)?.ToLower() ?? String.Empty).Equals("update"))
                    {
                        IsEntityFrameworkAction = true;
                    }
                    else if ((args.ElementAt(index + 1)?.ToLower() ?? String.Empty).Equals("drop"))
                    {
                        IsEntityFrameworkAction = true;
                    }
                }

                if (!IsEntityFrameworkAction)
                {
                    index = Array.IndexOf(args, "migrations");

                    if (index >= 0 && args.Length > index)
                    {
                        if ((args.ElementAt(index + 1)?.ToLower() ?? String.Empty).Equals("add") ||
                            (args.ElementAt(index + 1)?.ToLower() ?? String.Empty).Equals("remove"))
                        {
                            IsEntityFrameworkAction = true;
                        }
                    }
                }
            }

            if (Env.IsEnvironment("Testing") || IsEntityFrameworkAction)
            {
                ServiceCollectionExtensions.UseStaticRegistration = false;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AddServerHeader = false;
                options.AllowSynchronousIO = true;
                options.ApplicationSchedulingMode = SchedulingMode.Inline;
            });

            services.AddDistributedMemoryCache();

            services.AddAutoMapper(options =>
            {
                options.AddProfile<EntityToModelProfile>();
                options.AddProfile<ModelToEntityProfile>();
            });

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = new QueryStringApiVersionReader();
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            if (!String.IsNullOrEmpty(AppDbContext.ConnectionString) && !Env.IsEnvironment("Testing"))
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(AppDbContext.ConnectionString,
                        sqlServerOptions =>
                        {
                            sqlServerOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                            sqlServerOptions.MigrationsHistoryTable("MigrationsHistory");
                        }));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options =>
                    options.UseInMemoryDatabase(typeof(AppDbContext).Name));
            }

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Populate;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();

                app.UseDeveloperExceptionPage();
            }

            #region Logging to DB

            IDictionary<String, LogLevel> contextLogFilter = Configuration
                .GetSection("Logging:Context:LogLevel").GetChildren()
                .Where(i => !i.Key.Equals("Default", StringComparison.OrdinalIgnoreCase))
                .Select(i =>
                {
                    if (Enum.TryParse(i.Value, true, out LogLevel level))
                    {
                        return new KeyValuePair<String, LogLevel>(i.Key, level);
                    }

                    return new KeyValuePair<String, LogLevel>(i.Key, LogLevel.None);
                })
                .ToDictionary(k => k.Key, v => v.Value);

            LogLevel minLevel = Configuration.GetValue<LogLevel>("Logging:Context:LogLevel:Default");
            if ((contextLogFilter?.Count ?? 0) > 0)
            {
                loggerFactory.AddContext((category, logLevel) => {
                    if (contextLogFilter
                        .Any(f => category.Equals(f.Key, StringComparison.OrdinalIgnoreCase) ||
                            category.StartsWith(f.Key, StringComparison.OrdinalIgnoreCase)))
                    {
                        return logLevel >= contextLogFilter
                            .First(f => category.Equals(f.Key, StringComparison.OrdinalIgnoreCase) ||
                                category.StartsWith(f.Key, StringComparison.OrdinalIgnoreCase)).Value;
                    }

                    return logLevel >= minLevel;
                });
            }
            else
            {
                loggerFactory.AddContext(minLevel);
            }

            #endregion

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
