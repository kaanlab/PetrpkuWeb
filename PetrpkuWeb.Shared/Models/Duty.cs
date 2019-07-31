using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Duty
    {
        public int DutyId { get; set; }
        public DateTime DayOfDuty { get; set; }

        // relationship
        public int UserId { get; set; }
        public UserInfo AssignedTo { get; set; }
    }
}
