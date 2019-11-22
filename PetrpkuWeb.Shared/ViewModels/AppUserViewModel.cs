using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class AppUserViewModel
    {
        public int AppUserId { get; set; }
        public string Avatar { get; set; } 
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
        public AppUserIdentityViewModel AuthIdentity { get; set; }
        public IEnumerable<Duty> DaysOfDuty { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Message> Messages { get; set; }
        public DepartmentViewModel Department { get; set; }  
        public BuildingViewModel Building { get; set; }
    }
}
