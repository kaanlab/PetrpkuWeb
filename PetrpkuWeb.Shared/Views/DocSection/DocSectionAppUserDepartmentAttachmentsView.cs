using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class DocSectionAppUserDepartmentAttachmentsView
    {
        public int DocSectionId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        // relationship
        public AppUserView AppUserView { get; set; }
        public DepartmentView DepartmentView { get; set; }
        public List<AttachmentView> AttachmentsView { get; set; }
    }
}
