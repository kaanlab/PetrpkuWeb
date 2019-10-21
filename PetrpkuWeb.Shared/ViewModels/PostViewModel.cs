using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class PostViewModel
    {
        public string Poster { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        public int AppUserId { get; set; }
        public int DepartmentId { get; set; }
        public List<Attachment> Attachments { get; set; }
    }
}
