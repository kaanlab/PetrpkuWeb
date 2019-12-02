using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class MilRequestView
    {
        public int MilRequestId { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ToDo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsReadonly { get; set; }
        public AppUserView  AppUserView { get; set; }
        public SiteSectionView SiteSectionView { get; set; }
        public SiteSubSectionView SiteSubSectionView { get; set; }
        public List<AttachmentView> AttachmentsView { get; set; }
        public ApprovedView ApprovedView { get; set; }
        public CheckedView CheckedView { get; set; }
        public SentView SentView { get; set; }
        public PublishedView PublishedView { get; set; }
    }
}
