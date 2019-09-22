using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }

        // relationship
        public AppUser AppUser { get; set; }
    }
}
