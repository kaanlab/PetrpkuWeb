using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class PublishedViewModel
    {
        public int PublishedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublished { get; set; }
        public AppUserViewModel AppUserViewModel { get; set; }
        public MilRequestViewModel MilRequestViewModel { get; set; }
    }
}
