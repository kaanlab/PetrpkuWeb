using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Article
    {
        public enum Style
        {
            Standard,
            Info,
            Danger,
            Warning
        }

        public int ArticleId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public Style Type { get; set; }
        

        // relationship
        public int AppUserId { get; set; }
        public AppUser Author { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }

}
