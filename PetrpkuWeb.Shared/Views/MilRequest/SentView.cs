using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class SentView
    {
        public int SentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSent { get; set; }
        public AppUserView AppUserView { get; set; }
    }
}
