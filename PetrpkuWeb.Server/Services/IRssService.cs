using PetrpkuWeb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IRssService
    {
        Task<List<RssMil>> GetRssMilAndCache();
        Task<List<RssCalend>> GetRssCalendAndCache();
    }
}
