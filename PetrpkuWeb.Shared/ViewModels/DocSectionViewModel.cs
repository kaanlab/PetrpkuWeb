using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class DocSectionViewModel
    {
        public int DocSectionId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        // relationship
        public AppUserViewModel AppUserViewModel { get; set; }
        public DepartmentViewModel DepartmentViewModel { get; set; }
        public List<AttachmentViewModel> AttachmentsViewModel { get; set; }
    }
}
