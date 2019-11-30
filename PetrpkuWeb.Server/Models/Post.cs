using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Server.Models
{
    public class Post
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

        // relationship
        public AppUser AppUser { get; set; }
        public Department Department { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; } 

    }
}
