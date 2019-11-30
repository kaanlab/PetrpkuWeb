using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }

        // relationship
        public IEnumerable<AppUser> AppUsers { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
