using PetrpkuWeb.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class ArticleViewModel
    {
        //public enum Style
        //{
        //    [Display(Name = "Стандартное")] Standard,
        //    [Display(Name = "Информационное")] Info,
        //    [Display(Name = "Важное")] Danger,
        //    [Display(Name = "Особое")] Warning
        //}

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }

        public int CssTypeId { get; set; }
        public int AppUserId { get; set; }
        public List<Attachment> Attachments { get; set; } 
    }
}
