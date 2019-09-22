using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class Article
    {
        public enum Style
        {
            [Display(Name = "Стандартное")] Standard,
            [Display(Name = "Информационное")] Info,
            [Display(Name = "Важное")] Danger,
            [Display(Name = "Особое")] Warning
        }

        public int ArticleId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }
        public Style Type { get; set; }
        

        // relationship
        public int AppUserId { get; set; }
        public AppUser Author { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }

}
