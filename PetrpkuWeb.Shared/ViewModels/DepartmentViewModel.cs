using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
       public List<AppUserViewModel> ListOfUsers { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
