using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mittagessen.Web.Models;
using Mittagessen.Data.Interfaces;

namespace Mittagessen.Web.Validation
{
    public class UserModelValidation : AbstractValidator<UserModel>
    {
        private IUserRepository UserRepository { get; set; }

        public UserModelValidation(IUserRepository userRepository)
        {
            UserRepository = userRepository;

            //RuleFor(r => r.UserName).NotEmpty().WithMessage("Wählen Sie bitte einen Benutzernamen")
            //    .Length(1, 20).Must(UserNameAvailable).WithMessage("Der Benutzername wird schon benutzt");
            RuleFor(r => r.Email).Length(1, 40).EmailAddress()
                .Must(EmailAvailable).WithMessage("Die E-Mail-Adresse wurde schon von einem anderen Benutzer registriert");
        }

        private bool UserNameAvailable(UserModel instance, string name)
        {
            return UserRepository.UserNameAvailable(name, instance.UserId);
        }

        private bool EmailAvailable(UserModel instance, string email)
        {
            return UserRepository.EmailAddressAvailable(email, instance.UserId);
        }
    }
}