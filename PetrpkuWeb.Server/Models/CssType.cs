using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class CssType
    {
        public int CssTypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        //
        public IEnumerable<Note> Notes { get; set; }
    }
}
