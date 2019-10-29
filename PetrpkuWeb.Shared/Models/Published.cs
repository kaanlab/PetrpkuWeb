using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Published
    {
        public int PublishedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublished { get; set; }

        // relationship
        public int AppUserId { get; set; }
        public AppUser AssosiatedUser { get; set; }
    }
}
