using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Article
    {
        public enum Style
        {
            Standard,
            Warning,
            Danger,
            Info
        }

        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Style Name { get; set; }



        // relationship
        public int AppUserId { get; set; }
        public AppUser Author { get; set; }

        public List<Attachment> Attachments { get; set; }
    }

}
