using PetrpkuWeb.Server.Models;
using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IAppUsersService
    {
        Task<List<AppUser>> GetAll();
        Task<List<AppUser>> GetAllOrderById();
        Task<List<AppUser>> GetAllActiveOrderByDispalyName();
        Task<List<AppUser>> GetAllActiveDutiesOrderByDispalyName();
        Task<List<AppUser>> GetAllOrderByDispalyName();
        Task<AppUser> GetById(string appUserId);
        Task<List<AppUser>> GetBirthdaysForOneWeek(DateTime firstDayOfWeek, DateTime lastDayOfWeek);
        Task<AppUser> Add(IAuthUser authUser);
        Task<bool> Update(AppUser appUser);
        Task UpdateEmail(AppUser appUserIdentity, IAuthUser authUser);
        Task<AppUser> FindById(string appUserId);
        Task<bool> AddToRole(AppUser appUser, string appRole);
        Task<bool> RemoveFromRole(AppUser appUser, string appRole);
    }
}
