using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.PublicApi.Extensions
{
    public static class ServiceAuth
    {
        public static IServiceCollection ConfigureServiceAuth(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            var authType = configuration.GetValue<string>("Auth:Type");

            if (authType.Equals(AuthorizationConstants.AUTH_TYPE_AZURE_AD))
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.Audience = configuration.GetValue<string>("Auth:AzureAD:ResourceId");
                   options.Authority = $"{configuration.GetValue<string>("Auth:AzureAD:InstanceId")}{ configuration.GetValue<string>("Auth:AzureAD:TenantId")}";                   
               });
            }
            else
            {
                var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
                services.AddAuthentication(config =>
                {
                    config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            }

            return services;
        }
    }
}
