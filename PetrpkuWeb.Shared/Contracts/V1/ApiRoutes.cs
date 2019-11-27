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

            public const string ALL_LDAPUSERS = "api/v1/account/ldap/all";

            public const string ADD_IDENTITY = "api/v1/account/identity/add";

            public const string ALL_IDENTITIES = "api/v1/account/identity/all";
        }

        public static class Users
        {
            public const string ALL_ACTIVE = "api/v1/users/all/active";

            public const string ALL_ACTIVE_DUTIES = "api/v1/users/all/active-duty";

            public const string ALL = "api/v1/users/all";

            public const string USER = "api/v1/users/user";

            public const string BIRTHDAYS = "api/v1/users/birthdays";

            public const string UPDATE = "api/v1/users/update";
        }

        public static class Rss
        {
            public const string MILNEWS = "api/v1/rssfeed/petrpku-mil-ru";

            public const string CALEND = "api/v1/rssfeed/calend-ru";
        }

        public static class CssType
        {
            public const string ALL = "api/v1/csstype/all";

            public const string SHOW = "api/v1/csstype/show";

            public const string CREATE = "api/v1/csstype/create";

            public const string UPDATE = "api/v1/csstype/update";

            public const string DELETE = "api/v1/csstype/delete";
        }

        public static class Upload
        {
            public const string POSTER = "api/v1/upload/poster";

            public const string AVATAR = "api/v1/upload/avatar";

            public const string FILES = "api/v1/upload/files";

            public const string UPDATE = "api/v1/upload/update";

            public const string DELETE = "api/v1/upload/delete";
        }

        public static class Sections
        {
            public const string ALL_INCLUDE_SUBSECTIONS = "api/v1/sections/all";

            public const string ALL = "api/v1/sections/sitesections/all";

            public const string SHOW = "api/v1/sections/sitesection/show";

            public const string CREATE = "api/v1/sections/sitesection/create";

            public const string UPDATE = "api/v1/sections/sitesection/update";

            public const string DELETE = "api/v1/sections/sitesection/delete";

            public const string SUBSECTIONS = "api/v1/sections/sitesubsections";

            public const string SUBSECTION_ALL = "api/v1/sections/sitesubsections/all";

            public const string SUBSECTION_SHOW = "api/v1/sections/sitesubsection/show";

            public const string SUBSECTION_CREATE = "api/v1/sections/sitesubsection/create";

            public const string SUBSECTION_UPDATE = "api/v1/sections/sitesubsection/update";

            public const string SUBSECTION_DELETE = "api/v1/sections/sitesubsection/delete";
        }

    }
}
