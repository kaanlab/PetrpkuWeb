using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class SentViewModel
    {
        public int SentId { get; set; }
        public DateTime Date { get; set; }
        public bool IsSent { get; set; }
        public AppUserViewModel AppUserViewModel { get; set; }
    }
}
