using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace CreditDecision.Services
{
    public class KeyVaultService
    {
        private readonly SecretClient _secretClient;

        public KeyVaultService(IConfiguration configuration)
        {
            string keyVaultUrl = configuration["KeyVaultUrl"];
            _secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
        }

        public async Task<string> GetKeyValutValueAsync(string strSecretKey)
        {
            KeyVaultSecret secret = await _secretClient.GetSecretAsync(strSecretKey);
            return secret.Value;
        }
    }
}
