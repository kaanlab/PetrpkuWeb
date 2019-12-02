using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class BuildingAppUserView
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public List<AppUserView> AppUsersView { get; set; }
    }
}
