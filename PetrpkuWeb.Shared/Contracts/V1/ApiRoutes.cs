using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Contracts.V1
{
    public static class ApiRoutes
    {
        public static class Account
        {
            public const string LOGIN = "api/v1/account/login";

            public const string SEARCH = "api/v1/account/search";

            public const string GETALL_LDAPUSERS = "api/v1/account/ldap/all";

            public const string ADD_IDENTITY = "api/v1/account/identity/add";

            public const string GETALL_IDENTITIES = "api/v1/account/identity/all";
        }

        public static class Users
        {
            public const string GETALL_ACTIVE = "api/v1/users/all/active";

            public const string GETALL_ACTIVE_DUTIES = "api/v1/users/all/active-duty";

            public const string GETALL = "api/v1/users/all";

            public const string GETUSER = "api/v1/users/user";

            public const string GETBIRTHDAYS = "api/v1/users/birthdays";

            public const string UPDATE = "api/v1/users/update";
        }

    }
}
