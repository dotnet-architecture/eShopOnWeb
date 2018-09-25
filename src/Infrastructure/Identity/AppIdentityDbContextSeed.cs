using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.eShopWeb.Infrastructure.Identity
{
	public class AppIdentityDbContextSeed
	{
		public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
		{
			var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
			await userManager.CreateAsync(defaultUser, "Pass@word1");
		}
	}
}