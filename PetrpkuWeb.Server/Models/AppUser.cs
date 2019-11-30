using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PetrpkuWeb.Server.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidleName { get; set; }
        public string WorkingPosition { get; set; }
        public string MobPhone { get; set; }
        public string IntPhone { get; set; }
        public string ExtPhone { get; set; }
        public string Room { get; set; }
        public DateTime Birthday { get; set; }
        public bool IsDuty { get; set; }
        public bool IsActive { get; set; }
        public bool LdapAuth { get; set; }

        // relationship
        public IEnumerable<Duty> DaysOfDuty { get; set; }
        public IEnumerable<Note> Notes { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<DocSection> DocSections { get; set; }

        public Department Department { get; set; }
        public Building Building { get; set; }

        public IEnumerable<MilRequest> MilRequests { get; set; }
        public IEnumerable<Approved> Approveds { get; set; }
        public IEnumerable<Checked> Checkeds { get; set; }
        public IEnumerable<Sent> Sents { get; set; }
        public IEnumerable<Published> Publisheds { get; set; }
    }
}
