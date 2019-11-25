using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IAppUsersService
    {
        Task<List<AppUser>> GetAllActiveOrderByDispalyName();
        Task<List<AppUser>> GetAllActiveDutiesOrderByDispalyName();
        Task<List<AppUser>> GetAllOrderByDispalyName();
        Task<AppUser> GetUserById(string appUserId);
        Task<List<AppUser>> GetBirthdaysForOneWeek(DateTime firstDayOfWeek, DateTime lastDayOfWeek);
        Task<bool> UpdateUser(AppUser appUser);
        Task<List<AppUser>> GetAllIdentityUsersOrderById();
        Task<AppUser> AddIdentityUser(IAuthUser authUser);
        Task<AppUser> FindByName(IAuthUser authUser);
        Task UpdateEmail(AppUser appUserIdentity, IAuthUser authUser);
        Task<List<AppUser>> GetAllIdentityUsers();
    }
}
