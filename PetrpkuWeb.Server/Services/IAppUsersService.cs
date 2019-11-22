using PetrpkuWeb.Server.Models;
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
        Task<AppUser> GetUserById(int appUserId);
        Task<List<AppUser>> GetBirthdaysForOneWeek(DateTime firstDayOfWeek, DateTime lastDayOfWeek);
        Task<bool> UpdateUser(AppUser appUser);
    }
}
