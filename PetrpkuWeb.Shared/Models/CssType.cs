using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class CssType
    {
        public int CssTypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        //
        public IEnumerable<Article> Articles { get; set; }
    }
}
