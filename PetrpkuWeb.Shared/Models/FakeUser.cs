﻿
namespace PetrpkuWeb.Shared.Models
{
    public class FakeUser : IAuthUser
        {
            public string UserName { get; set; }
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string[] Roles { get; set; }
        
    }
}
