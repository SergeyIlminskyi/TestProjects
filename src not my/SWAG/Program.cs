using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SWAG
{
    public class Program
    {
        public static Task Main(String[] args)
        {
            return CreateWebHostBuilder(args).Build().RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(String[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .PreferHostingUrls(true)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>();
    }
}
