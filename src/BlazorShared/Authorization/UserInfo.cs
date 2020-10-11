using System.Collections.Generic;

namespace BlazorShared.Authorization
{
    public class UserInfo
    {
        public static readonly UserInfo Anonymous = new UserInfo();
        public bool IsAuthenticated { get; set; }
        public string NameClaimType { get; set; }
        public string RoleClaimType { get; set; }
        public string Token { get; set; }
        public IEnumerable<ClaimValue> Claims { get; set; }
    }
}
