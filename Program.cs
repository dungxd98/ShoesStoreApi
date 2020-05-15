/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ShoesStoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoesStoreApi.Data;

namespace ShoesStoreApi {
    public class Program {
        public static void Main (string[] args) {
            var host = CreateHostBuilder (args).Build ();
            using (var scope = host.Services.CreateScope ()) {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ShoesStoreApiContext> ();
                try {
                    SeedData.Initialize (context).Wait ();
                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>> ();
                    logger.LogError ("an error occured", ex);
                }
            }
            host.Run ();
        }

        public static IHostBuilder CreateHostBuilder (string[] args) =>
            Host.CreateDefaultBuilder (args)
            .ConfigureWebHostDefaults (webBuilder => {
                webBuilder.UseStartup<Startup> ();
            });
    }
}
