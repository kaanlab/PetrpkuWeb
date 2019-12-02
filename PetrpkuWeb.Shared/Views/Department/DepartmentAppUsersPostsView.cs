using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class DepartmentAppUsersPostsView
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public List<AppUserView> AppUsersView { get; set; }
        public List<PostView> PostsView { get; set; }
    }
}
