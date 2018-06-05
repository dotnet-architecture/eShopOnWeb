using System.Security.Principal;

namespace Microsoft.eShopWeb.ApplicationCore.Interfaces
{
    public interface IIdentityParser<T>
    {
        T Parse(IPrincipal principal);
    }
}
