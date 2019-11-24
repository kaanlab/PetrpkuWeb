
using PetrpkuWeb.Shared.ViewModels;
using System.Collections.Generic;

namespace PetrpkuWeb.NovellDirectoryLdap
{
    public interface ILdapAuthenticationService
    {
        IAuthUser Login(string username, string password);
        IAuthUser Search(string username);
       List<IAuthUser> SearchAll();
    }
}
