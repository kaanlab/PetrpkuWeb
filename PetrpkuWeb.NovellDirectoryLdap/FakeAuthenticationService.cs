using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;

namespace PetrpkuWeb.NovellDirectoryLdap
{
    public class FakeAuthenticationService : IAppAuthenticationService
    {
        public IAuthUser Login(string username, string password)
        {
            return new FakeUser()
            {
                DisplayName = "Иванов И.И.",
                UserName = "fakeuser",
                Email = "mee@mee.ru",
                Roles = new[] { "webportal_admin" }
            };
        }

        public IAuthUser Search(string username)
        {
            throw new NotImplementedException();
        }

        public List<IAuthUser> SearchAll()
        {
            var list = new List<IAuthUser>()
            {
                new FakeUser() {
                    DisplayName = "Васечкин В.В.",
                    UserName = "vasechkin",
                    Email = "mee@mee.ru",
                    Roles = new[] { "admin" }
                },
                new FakeUser() {
                    DisplayName = "Петров П.П.",
                    UserName = "petrov",
                    Email = "mee@mee.ru",
                    Roles = new[] { "admin" }
                },
                new FakeUser()
                {
                    DisplayName = "Иванов И.И.",
                    UserName = "fakeuser3",
                    Email = "mee@mee.ru",
                    Roles = new[] { "admin" }
                }
            };
            return list;
        }
    }
}
