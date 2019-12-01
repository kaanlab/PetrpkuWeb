using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Checked
    {
        public int CheckedId { get; set; }
        public DateTime Date { get; set; }
        public bool IsChecked { get; set; }

        // relationship
        public AppUser AppUser { get; set; }
    }
}
