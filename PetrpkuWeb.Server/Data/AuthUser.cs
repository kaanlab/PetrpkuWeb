using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Data
{
    public class AuthUser : IdentityUser
    {
        public int? UserInfoId { get; set; }
    }
}
