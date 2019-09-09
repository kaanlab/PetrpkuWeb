using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class FileDataViewModel
    {
        public long Size { get; set; }
        public string Name { get; set; }
        //public string Extension { get; set; }
        public string Description { get; set; }
        public byte[] Bytes { get; set; }
        //public IFormFile File { get; set; }


    }
}
