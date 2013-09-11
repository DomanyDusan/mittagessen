using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using FluentValidation;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Models;

namespace Mittagessen.Web.Validation
{
    public class RegistrationModelValidation : AbstractValidator<RegistrationModel>
    {
        private IUserRepository UserRepository { get; set; }

        public RegistrationModelValidation(IUserRepository userRepository)
        {
            UserRepository = userRepository;

            RuleFor(r => r.RegistrationPassword).NotEmpty()
                .Equal(ConfigurationManager.AppSettings["UserPassword"])
                .WithMessage("Geben Sie bitte das Passwort ein, das Sie für die Registrierung erhalten haben");
            RuleFor(r => r.RegistrationName).NotEmpty().Must(UserNameAvailable);
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.NewPassword).NotEmpty();
            RuleFor(r => r.ConfirmPassword).Equal(r => r.NewPassword);
        }

        private bool UserNameAvailable(string name)
        {
            return UserRepository.GetUserByName(name) == null;
        }
    }
}