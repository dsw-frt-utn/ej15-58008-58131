using DSW2026Ej15.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DSW2026Ej15.Api.Filters
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationEx)
            {
                context.Result = new BadRequestObjectResult(new { error = validationEx.Message });
                context.ExceptionHandled = true;
            }
        }
    }
}
