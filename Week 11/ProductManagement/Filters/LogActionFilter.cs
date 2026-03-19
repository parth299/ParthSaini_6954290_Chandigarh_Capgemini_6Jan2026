using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ProductManagement.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            Console.WriteLine($"[LOG] Executing: {actionName} at {DateTime.Now}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            Console.WriteLine($"[LOG] Executed: {actionName} at {DateTime.Now}");
        }
    }
}