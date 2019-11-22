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
        public ICollection<SiteSubsection> SiteSubsections { get; set; }
    }
}
