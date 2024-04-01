using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation;
using ValidationException = Core.CrossCuttingConcerns.Exceptions.Types.ValidationException;

namespace Core.CrossCuttingConcerns.Validation.Tool;

public static class ValidationTool
{
    public static void Validate(IValidator validator, object entity)
    {
        var context = new ValidationContext<object>(entity);

        //IEnumerable<ValidationExceptionModel> errors = validator
        //       .Select(validator => validator.Validate(context))
        //       .SelectMany(result => result.Errors)
        //       .Where(failure => failure != null)
        //       .GroupBy(
        //          keySelector: p => p.PropertyName,
        //          resultSelector: (propertyName, errors) =>
        //             new ValidationExceptionModel { Property = propertyName, Errors = errors.Select(e => e.ErrorMessage) }
        //       ).ToList();

        //if (errors.Any())
        //    throw new ValidationException(errors);


        var result = validator.Validate(context);

        if (!result.IsValid)
        { 
            var errors = result.Errors.GroupBy(
                  keySelector: p => p.PropertyName,
                  resultSelector: (propertyName, errors) =>
                     new ValidationExceptionModel { Property = propertyName, Errors = errors.Select(e => e.ErrorMessage) }
               ).ToList();
            throw new ValidationException(errors);
        }


        //if (!result.IsValid)
        //{
        //    //var messages = result.Errors[0].ErrorMessage;
        //    //if (result.Errors.Count > 1)
        //    //{
        //    //    foreach (var item in result.Errors)
        //    //    {
        //    //        var messageClone = messages;
        //    //        messages = $"{messageClone} {item.ErrorMessage + 1}";
        //    //    }
        //    //}
        //    //throw new ValidationException(result.Errors);
        //    //throw result.Errors.Select(error => new ValidationException(error.ErrorMessage));

        //    // List<ValidationException> validationExceptions = new List<ValidationException>();


        //    foreach (var error in result.Errors)
        //    {
        //       throw new ValidationException(error.ErrorMessage);
        //    }

        //    //throw new AggregateException(validationExceptions);

        //   throw new ValidationException(result.Errors.Select(error => new ValidationException(error.ErrorMessage)).ToString());

        //}
    }
}