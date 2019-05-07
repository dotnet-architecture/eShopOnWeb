using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace Microsoft.eShopWeb.Web.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HttpUtility.UrlPathEncode(link)}'>link</a>");
        }
    }
}
