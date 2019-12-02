using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class SiteSectionView
    {
        public int SiteSectionId { get; set; }
        public string Name { get; set; }
        public List<SiteSubSectionView> SiteSubsectionsView { get; set; }
    }
}
