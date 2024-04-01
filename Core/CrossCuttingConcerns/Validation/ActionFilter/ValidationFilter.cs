//using Core.CrossCuttingConcerns.Exceptions.Types;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;

//namespace Core.Validation.ActionFilter;

//public class ValidationFilter : IAsyncActionFilter
//{
//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        if (!context.ModelState.IsValid)
//        {
//            var errors = context.ModelState
//                   .Where(predicate: x => x.Value.Errors.Any())
//                   .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
//                   .ToArray();

//            context.Result = new BadRequestObjectResult(errors);
//            return;
//        }

//        await next();
//    }
//    //public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    //{
//    //    if (!context.ModelState.IsValid)
//    //    {
//    //        var errors = context.ModelState
//    //               .Where(predicate: x => x.Value.Errors.Any())
//    //               .ToDictionary(e => e.Key, e => e.Value.Errors.Select(e => e.ErrorMessage))
//    //               .ToArray();

//    //        var validationExceptionModel = new ValidationExceptionModel();
//    //        foreach (var error in errors)
//    //        {
//    //            foreach (var subError in errors)
//    //            {
//    //                validationExceptionModel = new()
//    //                {
//    //                    Property = subError.Key,
//    //                    Errors = subError.Value
//    //                };
//    //            }
//    //        }
//    //        context.Result = new ValidationException(validationExceptionModel.Errors.);
//    //        return;
//    //    }

//    //    await next();
//    //}
//}