using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace entry_time_serveless.Functions.Functions
{
    public static class scheduleFunction
    {
        [FunctionName("scheduleFunction")]
        public static void Run([TimerTrigger("0 */59 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
