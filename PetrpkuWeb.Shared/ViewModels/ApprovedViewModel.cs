using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class ApprovedViewModel
    {
        public int ApprovedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }

        // relationship
        public AppUserViewModel AppUserViewModel { get; set; }
        public MilRequestViewModel MilRequestViewModel { get; set; }
    }
}
