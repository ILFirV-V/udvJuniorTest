using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace UssJuniorTest
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentNullException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }
            else if (context.Exception is ArgumentException)
            {
                context.Result = new BadRequestObjectResult(context.Exception.Message);
            }

            context.ExceptionHandled = true;
        }
    }
}
