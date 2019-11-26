using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetrpkuWeb.Server.Services;
using PetrpkuWeb.Shared.Contracts.V1;

namespace PetrpkuWeb.Server.Controllers.V1
{
    //[Route("api/rssfeed")]
    [ApiController]
    public class RssFeedController : ControllerBase
    {
        private readonly IRssService _rssService;
        public RssFeedController(IRssService rssService)
        {
            _rssService = rssService;
        }

        [HttpGet(ApiRoutes.Rss.MILNEWS)]
        public async Task<ActionResult> GetMilNews()
        {
            var response = await _rssService.GetRssMilAndCache();

            return Ok(response);
        }

        [HttpGet(ApiRoutes.Rss.CALEND)]
        public async Task<ActionResult> GetCalend()
        {
            var response = await _rssService.GetRssCalendAndCache();

            return Ok(response);
        }
    }
}