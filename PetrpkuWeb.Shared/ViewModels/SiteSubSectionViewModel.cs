using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class SiteSubSectionViewModel
    {
        public int SiteSubsectionId { get; set; }
        public string Title { get; set; }
        public SiteSectionViewModel SiteSectionViewModel { get; set; }
    }
}
