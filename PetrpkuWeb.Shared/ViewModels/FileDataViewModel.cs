﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class FileDataViewModel
    {
        public long Size { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Byte64String { get; set; }
        
        public byte[] ByteArray { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        //public IFormFile File { get; set; }


    }
}
