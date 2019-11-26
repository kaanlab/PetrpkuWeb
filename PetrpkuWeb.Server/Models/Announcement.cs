using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }

        [Required(ErrorMessage = "Поле \"Заголовок\" не может быть пустым")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" не может быть пустым")]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime? UpdateDate { get; set; }


        // relationship
        public int AppUserId { get; set; }
        public AppUser Author { get; set; }
        public int CssTypeId { get; set; }
        public CssType CssType { get; set; }
    }
}
