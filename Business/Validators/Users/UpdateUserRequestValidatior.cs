using Business.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Users;

public class UpdateUserRequestValidatior : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidatior()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.FirstName).MinimumLength(2);
        RuleFor(c => c.FirstName).EmailAddress();
    }
}
