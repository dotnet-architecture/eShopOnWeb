using System.Security.Claims;

namespace ApplicationCore.Entities
{
    public class ApplicationUser : ClaimsIdentity
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
