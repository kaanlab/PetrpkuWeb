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
        private readonly string _url = "http://petrpku.mil.ru/more/Novosti/rss";
        private readonly IMemoryCache _cache;

        public RssFeedController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet("cachedrss")]
        public async Task<ActionResult<List<RssNewsItem>>> GetCachedNews()
        {
            return await GetRssNewsAndCache();
        }

        private async Task<List<RssNewsItem>> GetRssNewsAndCache()
        {
            if (!_cache.TryGetValue("ListOfRssNews", out List<RssNewsItem> rssNews))
            {
                string response;
                using (var client = new HttpClient())
                {
                    response = await client.GetStringAsync(_url);
                }

                if (response != null)
                {
                    var document = XDocument.Parse(response);

                    rssNews = (from descendant in document.Descendants("item")
                               select new RssNewsItem
                               {
                                   Description = descendant.Element("description").Value,
                                   Title = descendant.Element("title").Value,
                                   Link = descendant.Element("link").Value,
                                   PublishDate = DateTime.Parse(descendant.Element("pubDate").Value),
                                   Enclosure = descendant.Element("enclosure").FirstAttribute.Value
                               }).Take(10).ToList();

                    _cache.Set("ListOfRssNews", rssNews, TimeSpan.FromMinutes(10));
                }
            }
            return rssNews;
        }
    }
}