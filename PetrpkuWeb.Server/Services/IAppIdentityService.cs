using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IAppIdentityService
    {
        Task<List<AppUserIdentity>> GetAllIdentityUsersOrderById();
        Task<AppUserIdentity> AddIdentityUser(IAuthUser authUser);
        Task<AppUserIdentity> FindByName(IAuthUser authUser);
        Task UpdateEmail(AppUserIdentity appUserIdentity, IAuthUser authUser);
        Task<List<AppUserIdentity>> GetAllIdentityUsers();
    }
}
