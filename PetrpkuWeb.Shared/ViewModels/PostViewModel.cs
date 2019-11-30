using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string Poster { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool OnMain { get; set; }
        public AppUserViewModel AppUserViewModel { get; set; }
        public DepartmentViewModel DepartmentViewModel { get; set; }
        public List<AttachmentViewModel> AttachmentsViewModel { get; set; }
    }
}
