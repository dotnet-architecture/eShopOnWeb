using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.ApplicationCore.Enums;
public enum EmailType
{
    ConfirmEmail = 1,
    ResetPassword,
    TwoFactorAuthentication,
    OfficialMails
}
