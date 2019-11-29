using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class MilRequestViewModel
    {
        public int MilRequestViewModelId { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ToDo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        //
        public AppUserViewModel  AppUser { get; set; }
        public SiteSectionViewModel SiteSection { get; set; }
        public SiteSubSectionViewModel SiteSubSection { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
    }
}
