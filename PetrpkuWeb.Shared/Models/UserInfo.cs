using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public string WorkingPosition { get; set; }
        public string Phone { get; set; }
        public string Office { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime Birthday { get; set; }

        // relationship
        public IEnumerable<Duty> DaysOfDuty { get; set; }
        public IEnumerable<NewsPost> NewsPosts { get; set; }

    }
}
