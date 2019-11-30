using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class BuildingViewModel
    {
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public List<AppUserViewModel> AppUsersViewModel { get; set; }
    }
}
