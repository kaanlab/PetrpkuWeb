using System;


namespace PetrpkuWeb.Shared.Views
{
    public class DutyAppUserView
    {
        public int DutyId { get; set; }
        public DateTime DayOfDuty { get; set; }
        public AppUserDepartmentBuildingView AppUserView { get; set; }
    }
}
