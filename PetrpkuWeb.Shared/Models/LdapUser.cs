
namespace PetrpkuWeb.Shared.Models
{
    public class LdapUser : IAuthUser
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}
