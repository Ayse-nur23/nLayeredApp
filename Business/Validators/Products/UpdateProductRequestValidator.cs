using Business.Dtos.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Products;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {

        RuleFor(c => c.ProductName).NotEmpty();
        RuleFor(c => c.ProductName).MinimumLength(5);
        RuleFor(c => c.ProductName).EmailAddress();
    }
}
