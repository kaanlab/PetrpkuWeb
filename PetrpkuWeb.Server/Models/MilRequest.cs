using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetrpkuWeb.Server.Models
{
    public class MilRequest
    {
        public int MilRequestId { get; set; }

        [Required(ErrorMessage = "Это поле обязательно для заполнения!")]
        public string ToDo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsReadonly { get; set; }

        //
        public AppUser AppUser { get; set; }
        public SiteSection SiteSection { get; set; }
        public SiteSubsection SiteSubSection { get; set; }
        public IEnumerable<Attachment> Attachments { get; set; }
        public Approved Approved { get; set; }
        public Checked Checked { get; set; }
        public Sent Sent { get; set; }
        public Published Published { get; set; }
    }
}
