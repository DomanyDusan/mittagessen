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

            //string regPasswordRequired = "Geben Sie bitte das Kennwort ein, das Sie für die Registrierung erhalten haben";
            //RuleFor(r => r.RegistrationPassword).NotEmpty().WithMessage(regPasswordRequired)
            //    .Equal(ConfigurationManager.AppSettings["UserPassword"]).WithMessage(regPasswordRequired);
            RuleFor(r => r.RegistrationName).NotEmpty().WithMessage("Wählen Sie bitte einen Benutzernamen")
                .Length(1,20).Must(UserNameAvailable).WithMessage("Der Benutzername wird schon benutzt");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Bitte geben Sie eine E-Mail-Adresse ein. Die Adresse kann dann zum Login benutzt werden wenn Sie den Benutzernamen vergessen")
                .Length(1, 40).EmailAddress()
                .Must(EmailAvailable).WithMessage("Die E-Mail-Adresse wurde schon von einem anderen Benutzer registriert");
            RuleFor(r => r.NewPassword).Must(PasswordNotEmpty).WithMessage("Bitte geben Sie ein Passwort ein");
            RuleFor(r => r.ConfirmPassword).Equal(r => r.NewPassword);
            //RuleFor(r => r.UseDefaultPassword);
        }

        private bool UserNameAvailable(string name)
        {
            return UserRepository.UserNameAvailable(name);
        }

        private bool EmailAvailable(string email)
        {
            return UserRepository.EmailAddressAvailable(email);
        }

        private bool PasswordNotEmpty(RegistrationModel instance, string password)
        {
            return //instance.UseDefaultPassword || 
                !string.IsNullOrEmpty(password);
        }
    }
}