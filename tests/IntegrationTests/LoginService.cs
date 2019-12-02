using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.eShopWeb.Infrastructure.Identity;
using System;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.eShopWeb.Infrastructure.Logging;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Microsoft.eShopWeb.IntegrationTests.Repositories.OrderRepositoryTests
{
    public class LoginService
    {
        [Fact]
        public async Task LogsInSampleUser()
        {
            var services = new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase();

            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("Identity");
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                        .AddDefaultTokenProviders();

            services.AddTransient(typeof(ILogger<>), (typeof(Logger<>)));

            var serviceProvider = services
                .BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            // context (AppIdentityDbContext).
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                try
                {
                    // seed sample user data
                    var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                    AppIdentityDbContextSeed.SeedAsync(userManager).Wait();

                    var signInManager = scopedServices.GetRequiredService<SignInManager<ApplicationUser>>();

                    var email = "demouser@microsoft.com";
                    var password = "Pass@word1";

                    var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);

                    Assert.True(result.Succeeded);

                }
                catch (Exception ex)
                {
                }
            }

        }


    }
}
