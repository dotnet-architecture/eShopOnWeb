using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorShared.Authorization
{
    public static class Constants
    {
        public static class Roles
        {
            public const string ADMINISTRATORS = "Administrators";
        }

        public static bool InDocker = false;
    }
}
