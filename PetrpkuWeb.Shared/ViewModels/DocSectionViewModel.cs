using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class DocSectionViewModel
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        // relationship
        public string Id { get; set; }
        public AppUserViewModel Author { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentViewModel Department { get; set; }
        public List<AttachmentViewModel> Attachments { get; set; }
    }
}
