﻿
namespace PetrpkuWeb.NovellDirectoryLdap
{
    public class FakeUser : IAuthUser
        {
            public string Username { get; set; }
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string[] Roles { get; set; }
        
    }
}
