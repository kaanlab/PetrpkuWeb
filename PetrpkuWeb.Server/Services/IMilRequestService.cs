using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IMilRequestService
    {
        Task<List<MilRequest>> GetAll();
        Task<bool> Create(MilRequest milRequest);
        Task<MilRequest> GetOne(int milRequestId);
    }
}
