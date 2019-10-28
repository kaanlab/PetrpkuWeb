using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class SiteSubsection
    {
        public int SiteSubsectionId { get; set; }
        public string Title { get; set; }

        //
        public int SiteSectionId { get; set; }
        public SiteSection Section { get; set; }
    }
}
