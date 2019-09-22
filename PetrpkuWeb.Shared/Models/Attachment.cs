using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }

        // relationship
        public int? ArticleId { get; set; }
        public Article Article { get; set; }
        //public int? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
