using Business.Dtos.Categories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Categories;

public class UpdateCategoryRequestValidatior : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryRequestValidatior()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Name).MinimumLength(2);
    }
}
