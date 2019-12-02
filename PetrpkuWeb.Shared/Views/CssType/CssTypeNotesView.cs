using System;
using System.Collections.Generic;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class CssTypeNotesView
    {
        public int CssTypeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public List<NoteCssTypeView> NotesView { get; set; }
    }
}
