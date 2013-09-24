using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Mittagessen.Data.Interfaces;
using Mittagessen.Web.Models;

namespace Mittagessen.Web.Validation
{
    public class PasswordChangeValidation : AbstractValidator<PasswordChangeModel>
    {
        private IUserRepository UserRepository { get; set; }

        public PasswordChangeValidation(IUserRepository userRepository)
        {
            UserRepository = userRepository;

            RuleFor(p => p.OldPassword).NotEmpty();
            RuleFor(p => p.NewPassword).NotEmpty();
            RuleFor(p => p.ConfirmPassword).Equal(p => p.NewPassword);
        }
    }
}