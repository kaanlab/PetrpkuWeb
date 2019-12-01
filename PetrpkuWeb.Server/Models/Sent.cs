using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Sent
    {
        public int SentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSent { get; set; }

        // relationship
        public AppUser AppUser { get; set; }
    }
}
