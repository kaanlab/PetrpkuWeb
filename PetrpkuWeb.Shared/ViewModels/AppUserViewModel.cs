using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
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
        //public AppUserIdentityViewModel AuthIdentity { get; set; }
        public List<DutyViewModel> DaysOfDutyViewModel { get; set; }
        public List<NoteViewModel> NotesViewModel { get; set; }
        public List<PostViewModel> PostsViewModel { get; set; }
        public List<MilRequestViewModel> MilRequestsViewModel { get; set; }
        public DepartmentViewModel DepartmentViewModel { get; set; }  
        public BuildingViewModel BuildingViewModel { get; set; }
    }
}
