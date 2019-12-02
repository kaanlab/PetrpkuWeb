using PetrpkuWeb.Shared.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Client.Extensions
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginViewModel loginModel);
        Task Logout();
    }
}
