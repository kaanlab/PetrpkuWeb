using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace PetrpkuWeb.Shared.Models
{
    public class AppUserIdentity : IdentityUser
    {
        public string DisplayName { get; set; }

        // relationship
        public int AppUserId { get; set; }
        public AppUser AssosiatedUser { get; set; }
        
    }
}
