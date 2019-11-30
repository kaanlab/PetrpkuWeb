using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class SiteSectionViewModel
    {
        public int SiteSectionId { get; set; }
        public string Name { get; set; }
        public List<SiteSubSectionViewModel> SiteSubsectionsViewModel { get; set; }
    }
}
