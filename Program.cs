using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.AspNetCore.DataProtection;



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



SecretClientOptions options = new SecretClientOptions()
    {
        Retry =
        {
            Delay= TimeSpan.FromSeconds(2),
            MaxDelay = TimeSpan.FromSeconds(16),
            MaxRetries = 5,
            Mode = RetryMode.Exponential
         }
    };
var client = new SecretClient(new Uri("https://aggregator1-eastasia-kv.vault.azure.net/"), new DefaultAzureCredential(),options);

//KeyVaultSecret secret = client.GetSecret("ClientSecret");
KeyVaultSecret secret = client.GetSecret("MyNewSecret");


string secretValue = secret.Value;

//string secretValue = "ddddddd";


app.MapGet("/", () => "Hello World!!!! " + secretValue);

app.Run();
