using System;

namespace PetrpkuWeb.NovellDirectoryLdap
{
    public class FakeAuthenticationService : IAppAuthenticationService
    {
        public IAuthUser Login(string username, string password)
        {
            return new FakeUser()
            {
                DisplayName = "Иванов И.И.",
                Username = "fakeuser2",
                Roles = new [] {"admin"}
            };
        }

        public IAuthUser Search(string username)
        {
            throw new NotImplementedException();
        }
    }
}
