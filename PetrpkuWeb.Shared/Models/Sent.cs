using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Sent
    {
        public int SentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSent { get; set; }

        // relationship
        public int AppUserId { get; set; }
        public AppUser User { get; set; }
    }
}
