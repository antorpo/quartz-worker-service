using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace AR.WorkerService.Jobs
{
    [DisallowConcurrentExecution]
    public class ExampleJob2 : IJob
    {
        private readonly ILogger<ExampleJob2> _logger;

        public ExampleJob2(ILogger<ExampleJob2> logger)
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
