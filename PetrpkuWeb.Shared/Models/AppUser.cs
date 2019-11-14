using System;
using System.Collections.Generic;

namespace PetrpkuWeb.Shared.Models
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string Avatar { get; set; } = @"/img/user/default_avatar.png";
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public string WorkingPosition { get; set; }
        public string MobPhone { get; set; }
        public string IntPhone { get; set; }
        public string ExtPhone { get; set; }
        public string Room { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }
        public bool IsDuty { get; set; }
  

        // relationship
        public AppUserIdentity AuthIdentity { get; set; }
        public IEnumerable<Duty> DaysOfDuty { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Message> Messages { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        public int? BuildingId { get; set; }
        public Building Building { get; set; }
    }
}
