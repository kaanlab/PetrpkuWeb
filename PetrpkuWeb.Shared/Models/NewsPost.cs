using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class NewsPost
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        // relationship
        public int UserId { get; set; }
        public UserInfo Author { get; set; }
    }
}
