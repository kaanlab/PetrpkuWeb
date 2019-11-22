using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Building
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }
        //public bool IsHidden { get; set; }

        // relationship
        public List<AppUser> ListOfUsers { get; set; }
    }
}
