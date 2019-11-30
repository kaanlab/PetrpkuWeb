using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class AttachmentViewModel
    {
        public int AttachmentId { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool IsImage { get; set; }

        // relationship
        public DocSectionViewModel DocSectionViewModel { get; set; }
        public PostViewModel PostViewModel { get; set; }
        public MilRequestViewModel MilRequestViewModel { get; set; }
    }
}
