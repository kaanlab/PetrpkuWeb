using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Duty
    {
        public int DutyId { get; set; }
        public DateTime DayOfDuty { get; set; }

        // relationship
        public AppUser AppUser { get; set; }
    }
}
