using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Models
{
    public class DutyVM : IValidatableObject
    {
        
        public string Day { get; set; }
        public string User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Day))
                results.Add(new ValidationResult($"{nameof(Day)} cannot be -1, empty or consist of whitespace only", new[] { nameof(Day) }));

            if (string.IsNullOrEmpty(User))
                results.Add(new ValidationResult($"{nameof(User)} cannot be null, empty or consist of whitespace only", new[] { nameof(User) }));

            return results;
        }
    }
}
