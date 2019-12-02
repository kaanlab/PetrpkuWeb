using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class SiteSubSectionView
    {
        public int SiteSubsectionId { get; set; }
        public string Title { get; set; }
        public SiteSectionView SiteSectionView { get; set; }
    }
}
