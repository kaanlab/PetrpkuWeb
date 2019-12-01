using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Approved
    {
        public int ApprovedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }

        // relationship
        public AppUser AppUser { get; set; }
    }
}
