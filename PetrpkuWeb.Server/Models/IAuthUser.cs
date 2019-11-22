
namespace PetrpkuWeb.Server.Models
{
    public interface IAuthUser
    {
         string UserName { get; }
         string DisplayName { get; }
         string Email { get; }
         string[] Roles { get; }
    }
}
