using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ICETask3
{
    public class QueueFunction
    {
        private readonly ILogger<QueueFunction> _logger;

        public QueueFunction(ILogger<QueueFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueFunction))]
        [QueueOutput("output-queue")]
        public string[] Run
        (
            [QueueTrigger("input-queue", Connection = "StorageConnection")]
            Album album,
            FunctionContext context
        )
        {
            string[] messages =
            {
                $"Album name = {album.Name}",
                $"Album description = {album.Description}"
            };

            _logger.LogInformation("{msg1},{msg2}", messages[0], messages[1]);

            // Queue Output messages
            return messages;
        }
    }
}
