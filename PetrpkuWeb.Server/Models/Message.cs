
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ToDo { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        public bool IsSent { get; set; }
        public bool IsPublished { get; set; }
        public bool IsReadonly { get; set; }

        //
        public int AppUserId { get; set; }
        public AppUser Author { get; set; }
        public int SiteSectionId { get; set; }
        public SiteSection Section { get; set; }
        public int? SiteSubsectionId { get; set; }
        public SiteSubsection Subsection { get; set; }
        public List<Attachment> Attachments { get; set; }
        public int? CheckedId { get; set; }
        public Checked Checked { get; set; }
        public int? SentId { get; set; }
        public Sent Sent { get; set; }
        public int? PublishedId { get; set; }
        public Published Published { get; set; }
    }
}
