using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Microsoft.eShopWeb.Infrastructure.Identity;
public interface IUserService
{
    Task AddExternalLoginsAsync(ApplicationUser applicationUser, ExternalLoginInfo externalLoginInfo);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<string> ConfirmEmailAsync(string userId, string code);
    Task<ApplicationUser> FindUserByEmailAsync(string email);
    Task<ApplicationUser> FindUserByIdAsync(string userId);
    Task<ApplicationUser> FindUserByUserNameAsync(string userId);
    Task<string> RegisterUserAsync(string email, string password, string? username = null);
    Task<string> RegisterUserAsync(string email, string firstName, string lastName, string imgUrl, string? username = null);
}
