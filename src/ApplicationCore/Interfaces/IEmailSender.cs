using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string subject, string message);
}
