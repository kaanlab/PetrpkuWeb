using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.FileReader;

namespace PetrpkuWeb.Client.ViewModels
{
    public class FileViewModel
    {
        //public IFileReaderRef FileRef { get; set; }
        public string Description { get; set; }
        //public IFileInfo FileInfo { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }

    }
}
