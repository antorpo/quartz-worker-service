using AR.WorkerService.Configuration;
using AR.WorkerService.Jobs;
using Microsoft.Extensions.Hosting;
using Quartz;

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

            return Host.CreateDefaultBuilder(args)
                .UseWindowsService(options => { // Install service in Windows
                    options.ServiceName = "ExampleServiceName";
                }) 
                .UseSystemd() // Install service in Linux
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
