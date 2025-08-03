using Azure.Storage.Queues;
using CreditDecision.Models;
using CreditDecision.Services;
using System.Text;
using System.Text.Json;

public class CustomerService
{
    private readonly IConfiguration _configuration;
    private readonly KeyVaultService _keyVaultService;

    public CustomerService(IConfiguration configuration, KeyVaultService keyVaultService)
    {
        _configuration = configuration;
        _keyVaultService = keyVaultService;
    }

    public async Task<bool> SendCustCreatedMailAsync(Customer customer)
    {
        string queueConnString = await _keyVaultService.GetKeyValutValueAsync("EmailQueueCS");
        string queueName = _configuration["EmailQueueName"];

        QueueClient queueClient = new QueueClient(queueConnString, queueName);
        await queueClient.CreateIfNotExistsAsync();

        string messageJson = JsonSerializer.Serialize(customer);
        //string encodedMessage = Convert.ToBase64String(Encoding.UTF8.GetBytes(messageJson));
        await queueClient.SendMessageAsync(messageJson);

        return true;
    }
}
