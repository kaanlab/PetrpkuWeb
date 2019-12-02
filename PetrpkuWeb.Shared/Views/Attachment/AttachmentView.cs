using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class AttachmentView
    {
        public int AttachmentId { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool IsImage { get; set; }
    }
}
