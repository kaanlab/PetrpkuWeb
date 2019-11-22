using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Extensions
{
    public static class AuthRoles
    {
        public const string ADMIN = "webportal_admin";
        public const string USER = "webportal_user";
        public const string KADRY = "webportal_kadry";
        public const string PUBLISHER = "webportal_publisher";
        public const string ADMIN_PUBLISHER = "webportal_admin, webportal_publisher";
        public const string ADMIN_KADRY = "webportal_admin, webportal_kadry";
        public const string ANY = "webportal_admin, webportal_kadry, webportal_user";
    }
}
