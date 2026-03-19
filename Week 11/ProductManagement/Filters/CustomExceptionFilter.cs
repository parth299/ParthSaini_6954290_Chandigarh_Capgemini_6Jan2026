using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace ProductManagement.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log error
            Console.WriteLine($"[ERROR] {context.Exception.Message}");

            // Friendly response
            context.Result = new ContentResult
            {
                Content = "Oops! Something went wrong. Please try again later.",
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}