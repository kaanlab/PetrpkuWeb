using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Attachment
    {
        public int AttachmentId { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool IsImage { get; set; }

        // relationship
        public Post Post { get; set; }
        public MilRequest MilRequest { get; set; }
        public DocSection DocSection { get; set; }
    }
}
