using Microsoft.Extensions.Caching.Memory;
using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PetrpkuWeb.Server.Services
{
    public class RssService : IRssService
    {
        private readonly string _urlMil = "http://petrpku.mil.ru/more/Novosti/rss";
        private readonly string _urlCalend = "https://www.calend.ru/img/export/today-events.rss";
        private readonly IMemoryCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;

        public RssService(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
        {
            _cache = memoryCache;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<RssMil>> GetRssMilAndCache()
        {
            if (!_cache.TryGetValue("ListOfRssMil", out List<RssMil> rssNews))
            {
                string response;
                var client = _httpClientFactory.CreateClient();
                response = await client.GetStringAsync(_urlMil);

                if (response is { })
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

        public async Task<List<RssCalend>> GetRssCalendAndCache()
        {
            if (!_cache.TryGetValue("ListOfRssCalend", out List<RssCalend> rssNews))
            {
                string response;
                var client = _httpClientFactory.CreateClient();
                response = await client.GetStringAsync(_urlCalend);

                if (response is { })
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
