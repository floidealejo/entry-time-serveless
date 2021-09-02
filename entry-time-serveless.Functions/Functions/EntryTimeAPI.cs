using entry_time_serveless.Common.Models;
using entry_time_serveless.Common.Responses;
using entry_time_serveless.Functions.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace entry_time_serveless.Functions.Functions
{
    public static class EntryTimeAPI
    {
        [FunctionName(nameof(CreateEntryTime))]
        public static async Task<IActionResult> CreateEntryTime(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "entryTime")] HttpRequest req,
            [Table("entryTime", Connection = "AzureWebJobsStorage")] CloudTable entryTimeTable,
            ILogger log)
        {
            log.LogInformation("Received a new entry.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            EntryTime entryTime = JsonConvert.DeserializeObject<EntryTime>(requestBody);

            if (string.IsNullOrEmpty(entryTime?.EntryDate.ToString()))
            {
                return new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = "the request must have a Entry date",

                });
            }

            if (string.IsNullOrEmpty(entryTime.ExitDate.ToString()))
            {
                return new BadRequestObjectResult(new Response
                {
                    IsSuccess = false,
                    Message = "the request must have a Exit date",

                });
            }

            //if (DateTime.Equals(entryTime.EntryDate, entryTime.EntryDate))
            //{
            //    return new BadRequestObjectResult(new Response
            //    {
            //        IsSuccess = false,
            //        Message = "the request shouldn't have two or more equals entry",

            //    });
            //}

            //if (DateTime.Equals(entryTime.ExitDate, entryTime.ExitDate))
            //{
            //    return new BadRequestObjectResult(new Response
            //    {
            //        IsSuccess = false,
            //        Message = "the request shouldn't have two or more equals exit",

            //    });
            //}

            EntryTimeEntity entryTimeEntity = new EntryTimeEntity
            {
                IdEmployed = entryTime.IdEmployed,
                EntryDate = DateTime.UtcNow,
                ExitDate = DateTime.UtcNow,
                Type = entryTime.Type,
                IsConsolidated = false,
                ETag = "*",
                PartitionKey = "ENTRYTIME",
                RowKey = Guid.NewGuid().ToString(),
            };
            TableOperation addOperation = TableOperation.Insert(entryTimeEntity);
            await entryTimeTable.ExecuteAsync(addOperation);
            string message = "New entry stored in table";
            log.LogInformation(message);
            return new OkObjectResult(new Response
            {
                IsSuccess = true,
                Message = message,
                Result = entryTimeEntity
            });
        }
    }
}
