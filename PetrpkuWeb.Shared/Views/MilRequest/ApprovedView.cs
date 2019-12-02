using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class ApprovedView
    {
        public int ApprovedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }
        public AppUserView AppUserView { get; set; }
    }
}
