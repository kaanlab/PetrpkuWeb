using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }

        // relationship
        public List<AppUser> ListOfUsers { get; set; }
        public List<Post> Posts { get; set; }
    }
}
