using Business.Dtos.Products;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validators.Products;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty().MinimumLength(2).MaximumLength(50);
        RuleFor(p => p.CategoryId).NotEmpty();
        RuleFor(p => p.UnitPrice).NotEmpty().GreaterThan(0);

        //RuleFor(c => c.QuantityPerUnit).NotEmpty();
        //RuleFor(c => c.QuantityPerUnit).MinimumLength(2);
        //RuleFor(c => c.QuantityPerUnit).EmailAddress();
        //RuleFor(c => c.QuantityPerUnit).MaximumLength(13);

    }
}
