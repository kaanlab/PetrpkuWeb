using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        // relationship
        public int UserInfoId { get; set; }
        public UserInfo Author { get; set; }
    }
}
