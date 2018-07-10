using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using Xunit;

namespace SWAG.Test.Controllers
{
    public abstract class ControllerBaseTest
    {
        private TestServer Server { get; set; }

        protected Data.AppDbContext Context { get; private set; }

        protected IDistributedCache Cache { get; private set; }

        protected HttpClient Client
        {
            get
            {
                HttpClient client = Server.CreateClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }
        }

        protected String Version { get; private set; }

        public ControllerBaseTest()
            : this("v1.0")
        { }

        public ControllerBaseTest(String version)
        {
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Version = $"v{Version.TrimStart('v')}";

            Server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(GetContentRoot())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build())
                .UseContentRoot(GetContentRoot())
                .UseStartup<Startup>());

            IServiceProvider services = Server.Host.Services.CreateScope().ServiceProvider;

            try
            {
                Context = services.GetRequiredService<Data.AppDbContext>();
                Cache = services.GetRequiredService<IDistributedCache>();
            }
            catch (Exception ex)
            {
                ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database and/or cache.");
            }
        }

        private String GetContentRoot()
        {
            return GetContentRoot(typeof(Startup).Assembly.GetName().Name);
        }

        private String GetContentRoot(String projectName)
        {
            Boolean isRunFromConsole = System.Reflection.Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant() == "xunit.console";

            var solutionRoot = new DirectoryInfo(isRunFromConsole ?
                Directory.GetCurrentDirectory() : ApplicationEnvironment.ApplicationBasePath) // netcoreapp2.1 folder
                .Parent // Debug or Release folder
                .Parent // bin folder
                .Parent // SWAG.Test folder
                .Parent.FullName; // src folder

            return Path.GetFullPath(Path.Combine(solutionRoot, projectName));
        }

        internal static void SpawnAndWait(IEnumerable<Action> actions)
        {
            List<Action> list = actions.ToList();
            ManualResetEvent[] handles = new ManualResetEvent[actions.Count()];
            for (Int32 i = 0; i < list.Count; i++)
            {
                handles[i] = new ManualResetEvent(false);
                Action currentAction = list[i];
                ManualResetEvent currentHandle = handles[i];

                void wrappedAction()
                {
                    try
                    {
                        currentAction();
                    }
                    finally
                    {
                        currentHandle.Set();
                    }
                };

                ThreadPool.QueueUserWorkItem(x => wrappedAction());
            }

            WaitHandle.WaitAll(handles);
        }

        internal static void SpawnAndWait(Action action, Int32 numberOfThreads)
        {
            SpawnAndWait(Enumerable.Repeat(action, numberOfThreads));
        }
    }
}
