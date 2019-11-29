using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class NoteViewModel
    {
        public int NoteId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? UpdateDate { get; set; }


        // relationship
        public AppUserViewModel Author { get; set; }
        public CssTypeViewModel CssType { get; set; }
    }
}
