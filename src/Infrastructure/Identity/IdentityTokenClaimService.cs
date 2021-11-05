using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.eShopWeb.Infrastructure.Identity;

public class IdentityTokenClaimService : ITokenClaimsService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityTokenClaimService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> GetTokenAsync(string userName)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);
        var user = await _userManager.FindByNameAsync(userName);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userName) };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
