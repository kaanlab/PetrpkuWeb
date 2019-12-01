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

            public const string BIRTHDAYS_WEEK = "api/v1/users/birthdays/week";

            public const string UPDATE = "api/v1/users/update";

            public const string ADD_TO_ROLE = "api/v1/users/addtorole";

            public const string REMOVE_FROM_ROLE = "api/v1/users/removefromrole";
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

        public static class Buildings
        {
            public const string ALL = "api/v1/buildings/all";

            public const string CREATE = "api/v1/buildings/create";

            public const string UPDATE = "api/v1/buildings/update";

            public const string DELETE = "api/v1/buildings/delete";
        }

        public static class Departments
        {
            public const string ALL = "api/v1/departments/all";

            public const string SHOW = "api/v1/departments/show";

            public const string CREATE = "api/v1/departments/create";

            public const string UPDATE = "api/v1/departments/update";

            public const string DELETE = "api/v1/departments/delete";
        }

        public static class Duty
        {
            public const string TODAY = "api/v1/duty/today";

            public const string MONTH = "api/v1/duty/month";

            public const string GETFILE = "api/v1/duty/getfile";

            public const string CREATE_MANY = "api/v1/duty/createmany";

            public const string CREATE = "api/v1/duty/create";

            public const string UPDATE = "api/v1/duty/update";

            public const string DELETE = "api/v1/duty/delete";
        }

        public static class Post
        {
            public const string ALL = "api/v1/posts/all";

            public const string DEPARTMENT = "api/v1/posts/bydepartment";

            public const string CREATE = "api/v1/posts/create";

            public const string SHOW = "api/v1/posts/show";

            public const string UPDATE = "api/v1/posts/update";

            public const string DELETE = "api/v1/posts/delete";
        }

        public static class MilRequest
        {
            public const string ALL = "api/v1/milrequest/all";

            public const string CREATE = "api/v1/milrequest/create";

            public const string SHOW = "api/v1/milrequest/show";

            public const string UPDATE = "api/v1/milrequest/update";

            public const string DELETE = "api/v1/milrequest/delete";
        }

        public static class Note
        {
            public const string ALL = "api/v1/notes/all";

            public const string SHOW = "api/v1/notes/show";

            public const string CREATE = "api/v1/notes/create";

            public const string UPDATE = "api/v1/notes/update";

            public const string DELETE = "api/v1/notes/delete";
        }

        public static class DocSection
        {
            public const string ALL = "api/v1/docsections/all";

            public const string SHOW = "api/v1/docsections/show";

            public const string CREATE = "api/v1/docsections/create";

            public const string UPDATE = "api/v1/docsections/update";

            public const string DELETE = "api/v1/docsections/delete";
        }

        public static class Roles
        {
            public const string ALL = "api/v1/roles/all";

            public const string SHOW = "api/v1/roles/show";

            //public const string CREATE = "api/v1/roles/create";

            //public const string UPDATE = "api/v1/roles/update";
        }
    }
}
