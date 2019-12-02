using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class PublishedView
    {
        public int PublishedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsPublished { get; set; }
        public AppUserView AppUserView { get; set; }
    }
}
