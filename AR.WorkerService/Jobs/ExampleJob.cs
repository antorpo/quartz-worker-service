using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace AR.WorkerService.Jobs
{
    public class ExampleJob : IJob
    {
        private readonly ILogger<ExampleJob> _logger;

        public ExampleJob(ILogger<ExampleJob> logger)
        {
            _logger = logger;
        }
 
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("{jobName} running at {time}", this.GetType().Name, DateTimeOffset.Now);
            return Task.CompletedTask;
        }
    }
}
