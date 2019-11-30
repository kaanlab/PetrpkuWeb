using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class SiteSubsection
    {
        public int SiteSubSectionId { get; set; }
        public string Title { get; set; }

        //
        public SiteSection SiteSection { get; set; }
    }
}
