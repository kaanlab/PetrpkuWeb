using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IDutyService
    {
        Task<Duty> GetOne(int dutyId);
        Task<Duty> DutyToday();
        Task<List<Duty>> DutyMonth(int selectedMonth, int selectedYear);
        Task<byte[]> GetFileAsync(int selectedMonth, int selectedYear);
        Task<bool> Create(Duty duty);
        Task<bool> Update(Duty duty);
        Task<bool> Delete(int dutyId);

    }
}
