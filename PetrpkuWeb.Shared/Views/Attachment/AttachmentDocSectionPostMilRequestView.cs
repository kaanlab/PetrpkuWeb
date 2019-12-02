using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class AttachmentDocSectionPostMilRequestView
    {
        public int AttachmentId { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public bool IsImage { get; set; }
        public DocSectionView DocSectionView { get; set; }
        public PostView PostView { get; set; }
        public MilRequestView MilRequestView { get; set; }
    }
}
