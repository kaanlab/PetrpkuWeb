using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Extensions
{
    public static class AuthRoles
    {
        public const string ADMIN = "Admin";
        public const string USER = "User";
        public const string KADRY = "Kadry";
        public const string PUBLISHER = "Publisher";
        public const string ADMIN_PUBLISHER = "Admin, Publisher";
        public const string ADMIN_KADRY = "Admin, Kadry";
        public const string ADMIN_KADRY_USER = "Admin, Kadry, User";
    }
}
