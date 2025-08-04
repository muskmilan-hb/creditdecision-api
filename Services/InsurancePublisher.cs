using Azure.Messaging.ServiceBus;
using CreditDecision.Models;
using Microsoft.Extensions.Azure;
using System.Text.Json;

namespace CreditDecision.Services
{
    public class InsurancePublisher(IConfiguration configuration)
    {
        private readonly string? _loanBusConnStr = configuration["ServiceBus:ConnectionString"];
        private readonly string? _loanBusQueueName = configuration["ServiceBus:QueueName"];

        public async Task PublishInsuranceAsync(InsuranceRequestedEvent insurance)
        {
            try
            {
                await using var client = new ServiceBusClient(_loanBusConnStr);
                var sender = client.CreateSender(_loanBusQueueName);

                var msgBody = JsonSerializer.Serialize(insurance);
                var msg = new ServiceBusMessage(msgBody);

                await sender.SendMessageAsync(msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

