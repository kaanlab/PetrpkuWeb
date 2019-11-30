using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetrpkuWeb.Shared.ViewModels
{
    public class LoginViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Поле \"Имя пользователя\" не может быть пустым")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Поле \"Пароль\" не может быть пустым")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var results = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Username))
                results.Add(new ValidationResult("Поле \"Имя пользователя\" не может быть пустым", new[] { nameof(Username) }));

            if (string.IsNullOrEmpty(Password))
                results.Add(new ValidationResult("Поле \"Пароль\" не может быть пустым", new[] { nameof(Password) }));

            return results;
        }
    }
}
