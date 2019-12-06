using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class AppUserRoleView
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public RoleView Role { get; set; }
    }
}
