using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;

namespace PetrpkuWeb.Client.ViewModel
{
    public class FileViewModel
    {
        //public string Name { get; set; }
        public long Size { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Byte64String { get; set; }
        public byte[] ByteArray { get; set; }
        //public IFileReference File { get; set; }
        public int Resize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
