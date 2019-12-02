using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PetrpkuWeb.Shared.Views
{
    public class FileInfoViewModel
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
