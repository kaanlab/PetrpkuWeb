using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class MilRequestViewModel
    {
        public int MilRequestId { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ToDo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsReadonly { get; set; }
        public AppUserViewModel  AppUserViewModel { get; set; }
        public SiteSectionViewModel SiteSectionViewModel { get; set; }
        public SiteSubSectionViewModel SiteSubSectionViewModel { get; set; }
        public List<AttachmentViewModel> AttachmentsViewModel { get; set; }
        public ApprovedViewModel ApprovedViewModel { get; set; }
        public CheckedViewModel CheckedViewModel { get; set; }
        public SentViewModel SentViewModel { get; set; }
        public PublishedViewModel PublishedViewModel { get; set; }
    }
}
