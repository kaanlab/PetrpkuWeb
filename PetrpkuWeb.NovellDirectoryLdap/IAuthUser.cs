
namespace PetrpkuWeb.NovellDirectoryLdap
{
    public interface IAuthUser
    {
         string Username { get; }
         string DisplayName { get; }
         string Email { get; }
         string[] Roles { get; }
    }
}
