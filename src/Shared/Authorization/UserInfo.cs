using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Shared.Authorization
{
    public class UserInfo
    {
        public static readonly UserInfo Anonymous = new UserInfo();
        public bool IsAuthenticated { get; set; }
        public string NameClaimType { get; set; }
        public string RoleClaimType { get; set; }
        public IEnumerable<ClaimValue> Claims { get; set; }
    }
}
