using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetrpkuWeb.Server.Data;
using PetrpkuWeb.Shared.Models;

namespace PetrpkuWeb.Server.Controllers
{
    [Route("api/rssfeed")]
    [ApiController]
    public class RssFeedController : ControllerBase
    {
        private readonly string _urlMil = "http://petrpku.mil.ru/more/Novosti/rss";
        private readonly string _urlCalend = "https://www.calend.ru/img/export/today-events.rss";
        private readonly IMemoryCache _cache;

        public RssFeedController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet("mil")]
        public async Task<ActionResult<List<RssMil>>> GetCachedMil()
        {
            return await GetRssMilAndCache();
        }

        [HttpGet("calend")]
        public async Task<ActionResult<List<RssCalend>>> GetCachedCalend()
        {
            return await GetRssCalendAndCache();
        }

        private async Task<List<RssMil>> GetRssMilAndCache()
        {
            if (!_cache.TryGetValue("ListOfRssMil", out List<RssMil> rssNews))
            {
                string response;
                using (var client = new HttpClient())
                {
                    response = await client.GetStringAsync(_urlMil);
                }

                if (response != null)
                {
                    var document = XDocument.Parse(response);

                    rssNews = (from descendant in document.Descendants("item")
                               select new RssMil
                               {
                                   Description = descendant.Element("description").Value,
                                   Title = descendant.Element("title").Value,
                                   Link = descendant.Element("link").Value,
                                   PublishDate = DateTime.Parse(descendant.Element("pubDate").Value),
                                   Enclosure = descendant.Element("enclosure").FirstAttribute.Value
                               }).Take(10).ToList();

                    _cache.Set("ListOfRssMil", rssNews, TimeSpan.FromMinutes(10));
                }
            }
            return rssNews;
        }

        private async Task<List<RssCalend>> GetRssCalendAndCache()
        {
            if (!_cache.TryGetValue("ListOfRssCalend", out List<RssCalend> rssNews))
            {
                string response;
                using (var client = new HttpClient())
                {
                    response = await client.GetStringAsync(_urlCalend);
                }

                if (response != null)
                {
                    var document = XDocument.Parse(response);

                    rssNews = (from descendant in document.Descendants("item")
                        select new RssCalend()
                        {
                            Description = descendant.Element("description").Value,
                            Title = descendant.Element("title").Value,
                            Link = descendant.Element("link").Value,
                            Guid = descendant.Element("guid").Value
                        }).ToList();

                    _cache.Set("ListOfRssCalend", rssNews, TimeSpan.FromHours(1));
                }
            }
            return rssNews;
        }
    }
}