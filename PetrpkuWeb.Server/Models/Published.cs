using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Published
    {
        public int PublishedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublished { get; set; }
       

        // relationship
        public AppUser AppUser { get; set; }
    }
}
