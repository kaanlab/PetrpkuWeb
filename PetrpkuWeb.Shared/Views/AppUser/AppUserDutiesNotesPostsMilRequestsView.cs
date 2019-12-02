using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class AppUserDutiesNotesPostsMilRequestsView
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
        public List<DutyView> DutiesView { get; set; }
        public List<NoteCssTypeView> NotesView { get; set; }
        public List<PostAttachmentView> PostsView { get; set; }
        public List<MilRequestView> MilRequestsView { get; set; }
        public DepartmentView DepartmentView { get; set; }
        public BuildingView BuildingView { get; set; }
    }
}
