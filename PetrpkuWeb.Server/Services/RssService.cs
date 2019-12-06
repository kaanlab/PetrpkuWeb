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
        private readonly string urlMil = "http://petrpku.mil.ru/more/Novosti/rss";
        private readonly string urlCalend = "https://www.calend.ru/img/export/today-events.rss";
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;

        public RssService(IMemoryCache memoryCache, HttpClient httpClient)
        {
            _cache = memoryCache;
            _httpClient = httpClient;
        }
        public async Task<List<RssMil>> GetRssMilAndCache()
        {
            if (!_cache.TryGetValue("ListOfRssMil", out List<RssMil> rssNews))
            {
                             
                var response = await _httpClient.GetStringAsync(urlMil);

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

                var response = await _httpClient.GetStringAsync(urlCalend);

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
