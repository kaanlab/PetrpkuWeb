
namespace PetrpkuWeb.NovellDirectoryLdap
{
    public interface IAppAuthenticationService
    {
        IAuthUser Login(string username, string password);
        IAuthUser Search(string username);
    }
}
