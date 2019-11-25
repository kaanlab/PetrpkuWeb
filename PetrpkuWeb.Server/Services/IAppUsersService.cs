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

    }
}
