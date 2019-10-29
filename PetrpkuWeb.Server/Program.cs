using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetrpkuWeb.Server.Data;
using Serilog;

namespace PetrpkuWeb.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {

#if RELEASE
            // Init Serilog configuration
            var configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.log.json")
              .Build();

            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            try
            {
                Log.Information("Starting web host");
#endif
                var host = CreateHostBuilder(args).Build();

                // Initialize the database
                var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
                using (var scope = scopeFactory.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    if (!db.AppUsers.Any())
                        SeedData.Initialize(db);
                }

                host.Run();

#if RELEASE
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
#endif
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
#if RELEASE
                }).UseSerilog();
#else
                });
#endif
    }
}
