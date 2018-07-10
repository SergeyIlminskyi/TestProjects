using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace SWAG.Mvc
{
    [Route("v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract class ControllerBase<TController>
        : Controller
        where TController : Controller
    {
        protected ILogger<TController> Logger { get; private set; }

        protected IHostingEnvironment Env { get; private set; }

        protected IMapper Mapper { get; private set; }

        protected Data.AppDbContext Context { get; private set; }

        protected IDistributedCache Cache { get; private set; }

        public ControllerBase(IServiceProvider serviceProvider)
            : base()
        {
            Logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<TController>();
            Env = serviceProvider.GetRequiredService<IHostingEnvironment>();
            Mapper = serviceProvider.GetRequiredService<IMapper>();
            Context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<Data.AppDbContext>();
            Cache = serviceProvider.GetRequiredService<IDistributedCache>();
        }
    }
}
