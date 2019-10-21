using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Post
    {
        public int PostId { get; set; }

        public string Poster { get; set; } = "@/img/site/post.png";

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        // relationship
        public int AppUserId { get; set; }
        public AppUser Author { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public List<Attachment> Attachments { get; set; } 

    }
}
