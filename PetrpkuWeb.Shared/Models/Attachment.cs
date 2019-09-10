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

        // rel
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
