using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class CssTypeViewModel
    {
        public int CssTypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public List<NoteViewModel> NotesViewModels { get; set; }
    }
}
