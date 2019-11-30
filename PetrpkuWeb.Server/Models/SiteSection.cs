using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class SiteSection
    {
        public int SiteSectionId { get; set; }
        public string Name { get; set; }

        //
        public IEnumerable<SiteSubsection> SiteSubSections { get; set; }
    }
}
