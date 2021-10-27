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

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
