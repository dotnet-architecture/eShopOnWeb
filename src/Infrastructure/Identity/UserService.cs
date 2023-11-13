using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;


namespace Microsoft.eShopWeb.Infrastructure.Identity;
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        throw new NotImplementedException();
    }

    public Task<string> ConfirmEmailAsync(string userId, string code)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser> FindUserByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user;
    }

    public Task<ApplicationUser> FindUserByIdAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ApplicationUser> FindUserByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        return user;

    }

    public async Task AddExternalLoginsAsync(ApplicationUser applicationUser, ExternalLoginInfo externalLoginInfo)
    {
        var result = await _userManager.AddLoginAsync(applicationUser, externalLoginInfo);
        if (!result.Succeeded)
        {
            throw new ApplicationException($"Unexpected error occurred adding external login for user");
        }
    }


    public async Task<string> RegisterUserAsync(string email, string password, string? username = null)
    {
        //init the app user, first check if the username is null, then use the email as the username 
        string UserName = username ?? email;
        var user = new ApplicationUser { UserName = UserName, Email = email };


        try
        {
            //create the user 
            IdentityResult result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                //generate a token for this user to be confirmed in his email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                //encode the code to be passed over a URL
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                //get the domain name for the client connecting to avoi impersonation
                string domainName = _configuration.GetSection("baseUrls:webBase").Value ?? throw new Exception("check the appsetings configuration, no baseUrls:webBase property was set!");

                //construct the callback url
                var callbackUrl = $"{domainName}/Welcome/Confirm?code={code}&email={user.Email}";

                /**TODO: send a mail to the email used in registration to confirm if this user can be reached whenever we want to pass a message*/


                //use the email sender library
                //EmailModel EmailModel = new EmailModel
                //{
                //    Link = callbackUrl,
                //    ReceipientEmail = request.Email,
                //    EmailType = EmailType.ConfirmEmail,
                //};

                //await emailSenderRepo.SendMail3(EmailModel);

                return user.UserName;






            }
            else
            {
                throw new InvalidOperationException(result?.Errors?.FirstOrDefault()?.Description);

            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<string> RegisterUserAsync(string email, string firstName, string lastName, string imgUrl, string? username = null)
    {

        try
        {

            //init the app user, first check if the username is null, then use the email as the username 
            string UserName = username ?? email;
            ApplicationUser user = new ApplicationUser { UserName = UserName, Email = email, FirstName = firstName, LastName = lastName, ImgUrl = imgUrl, EmailConfirmed = true };

            //create the user 
            IdentityResult result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {              

                return user.UserName;

            }
            else
            {
                throw new InvalidOperationException(result?.Errors?.FirstOrDefault()?.Description);

            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }


}
