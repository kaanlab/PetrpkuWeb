using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Published
    {
        public int PublishedId { get; set; }
        public DateTime Date { get; set; }

        // relationship
        public int AppUserId { get; set; }
        public AppUser AssosiatedUser { get; set; }
    }
}
