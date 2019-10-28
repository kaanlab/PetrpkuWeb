using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Message
    {
        public int MassageId { get; set; }
        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

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
