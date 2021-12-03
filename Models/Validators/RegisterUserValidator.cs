using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneySaverAPI.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator(MoneySaverDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.Password).MinimumLength(6);

            RuleFor(x => x.ConfirmPassword).Equal(e=> e.Password);

            //RuleFor(x => x.Email).Custom((value, context) =>
            //{
            //    var emailInUse = dbContext.Expense.Any(u => u.ExpenseName == value);
            //    if (emailInUse)
            //    {
            //        context.AddFailure("Email", "That email is taken");
            //    }
            //});

        }
    }
}
