using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class MessageViewModel
    {
        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ToDo { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        //
        public int AppUserId { get; set; }
        public int SiteSectionId { get; set; }
        public int? SiteSubsectionId { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
