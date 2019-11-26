using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        // relationship
        public string Id { get; set; }
        public AppUser Author { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
