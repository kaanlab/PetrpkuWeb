using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Checked
    {
        public int CheckedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsChecked { get; set; }

        // relationship
        public int AppUserId { get; set; }
        public AppUser AssosiatedUser { get; set; }
    }
}
