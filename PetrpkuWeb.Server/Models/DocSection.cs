using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Models
{
    public class DocSection
    {
        public int DocSectionId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        // relationship
        public AppUser Author { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
    }
}
