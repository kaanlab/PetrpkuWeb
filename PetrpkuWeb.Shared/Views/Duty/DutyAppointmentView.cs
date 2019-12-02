using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.Views
{
    public class DutyAppointmentView : IValidatableObject
    {
        [Required(ErrorMessage = "Поле \"Дата\" не может быть пустым")]
        public string Day { get; set; }
        [Required(ErrorMessage = "Поле \"Дежурный\" не может быть пустым")]
        public string User { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Day))
                results.Add(new ValidationResult("Поле \"Дата\" не может быть пустым", new[] { nameof(Day) }));

            if (string.IsNullOrEmpty(User))
                results.Add(new ValidationResult("Поле \"Дежурный\" не может быть пустым", new[] { nameof(User) }));

            return results;
        }
    }
}
