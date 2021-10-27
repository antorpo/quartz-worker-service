using AR.WorkerService.Configuration;
using AR.WorkerService.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Quartz;
using Serilog;

namespace AR.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // https://www.fixedbuffer.com/worker-service-como-crear-un-servicio-net-core-3-multiplataforma/
        public static IHostBuilder CreateHostBuilder(string[] args) {

            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            return Host.CreateDefaultBuilder(args)
                .UseWindowsService(options => { // Install service in Windows
                    options.ServiceName = "ExampleServiceName";
                }) 
                .UseSystemd() // Install service in Linux
                .UseSerilog((context, configuration) =>
                
                    // Replace the default ASP.NET Core logging system to use Serilog
                    configuration.ReadFrom.Configuration(config)
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddQuartz(q => {

                        q.UseMicrosoftDependencyInjectionJobFactory();

                        q.AddJobAndTrigger<ExampleJob>(hostContext.Configuration);
                        q.AddJobAndTrigger<ExampleJob2>(hostContext.Configuration);
                    });

                    services.AddQuartzHostedService(
                       q => q.WaitForJobsToComplete = true);
                });
        }
           
    }
}
