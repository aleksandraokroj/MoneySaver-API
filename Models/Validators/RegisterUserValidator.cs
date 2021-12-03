using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaverAPI.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<User>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e=> e.Password);

        }
    }
}
