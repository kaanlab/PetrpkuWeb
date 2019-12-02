using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class CheckedView
    {
        public int CheckedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsChecked { get; set; }
        public AppUserView AppUserView { get; set; }
    }
}
