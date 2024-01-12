using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators;

public class CreateProductRequestValidation : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidation()
    {
        RuleFor(c => c.ProductName).NotEmpty();
        RuleFor(c => c.ProductName).MinimumLength(2);
        RuleFor(c => c.ProductName).EmailAddress();
    }
}
