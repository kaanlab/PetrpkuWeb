using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class RssMil
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Enclosure { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
