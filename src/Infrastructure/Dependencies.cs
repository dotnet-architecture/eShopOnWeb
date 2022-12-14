using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using System;

namespace Microsoft.eShopWeb.Infrastructure;

public static class Dependencies
{
    public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        var useOnlyInMemoryDatabase = false;
        string keyVaultUri = configuration["KEY_VAULT_ENDPOINT"];
        string catalogConnectionStringKey = configuration["AZURE-SQL-CATALOG-CONNECTION-STRING"];
        string identityConnectionStringKey = configuration["AZURE-SQL-IDENTITY-CONNECTION-STRING"];
        string catalogConnectionStringValue = GetSqlConnectString(keyVaultUri, catalogConnectionStringKey);
        string identityConnectionStringValue = GetSqlConnectString(keyVaultUri, identityConnectionStringKey);

        if (configuration["UseOnlyInMemoryDatabase"] != null)
        {
            useOnlyInMemoryDatabase = bool.Parse(configuration["UseOnlyInMemoryDatabase"]);
        }

        if (useOnlyInMemoryDatabase)
        {
            services.AddDbContext<CatalogContext>(c =>
               c.UseInMemoryDatabase("Catalog"));
         
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseInMemoryDatabase("Identity"));
        }
        else
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<CatalogContext>(c =>
                c.UseSqlServer(catalogConnectionStringValue));

            // Add Identity DbContext
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(identityConnectionStringValue));
        }
    }

    public static string GetSqlConnectString(string keyVaultUri, string connectionStringKey)
    {
        if (connectionStringKey == null)
        {
            return "";
        }

        var secretClient = new SecretClient(new Uri(keyVaultUri), new ClientSecretCredential("<tenant_id>","<client_id>","<client_secret>"));
        KeyVaultSecret secret = secretClient.GetSecret(connectionStringKey);
        string secretValue = secret.Value;
        return secretValue;
    }
}
