using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class UsersBirthdaysForWeek
    {
        public DateTime FirstDayOfWeek { get; set; }
        public DateTime LastDayOfWeek { get; set; }
        public IEnumerable<UserInfo> UsersBirthdays { get; set; }
    }
}
