using Business.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Users;

public class CreateUserRequestValidatior : AbstractValidator<UserForRegisterRequest>
{
    public CreateUserRequestValidatior()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).MinimumLength(2);
        RuleFor(c => c.Email).EmailAddress();
    }
}
