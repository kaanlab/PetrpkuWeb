using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class CheckedViewModel
    {
        public int CheckedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsChecked { get; set; }

        // relationship
        public AppUserViewModel AppUserViewModel { get; set; }
        public MilRequestViewModel MilRequestViewModel { get; set; }
    }
}
