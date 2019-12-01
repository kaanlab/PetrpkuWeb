using PetrpkuWeb.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Services
{
    public interface IRssService
    {
        Task<List<RssMil>> GetRssMilAndCache();
        Task<List<RssCalend>> GetRssCalendAndCache();
    }
}
