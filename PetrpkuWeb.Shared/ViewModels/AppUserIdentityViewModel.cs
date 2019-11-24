using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class AppUserIdentityViewModel
    {
        public string DisplayName { get; set; }
        public AppUserViewModel AssosiatedUser { get; set; }
    }
}
