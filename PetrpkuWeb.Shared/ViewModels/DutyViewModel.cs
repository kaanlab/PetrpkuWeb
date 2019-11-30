using System;


namespace PetrpkuWeb.Shared.ViewModels
{
    public class DutyViewModel 
    {
        public int DutyId { get; set; }
        public DateTime DayOfDuty { get; set; }
        public AppUserViewModel AppUserViewModel { get; set; }
    }
}
