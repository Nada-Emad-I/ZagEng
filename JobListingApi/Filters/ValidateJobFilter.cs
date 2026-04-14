using JobListingsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JobListingsAPI.Filters
{
    public class ValidateJobFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var job = context.ActionArguments.Values
                .OfType<JobListing>()
                .FirstOrDefault();

            if (job is null)
            {
                context.Result = new BadRequestObjectResult("Request body is missing.");
                return;
            }

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(job.Title))
                errors.Add("Title is required.");

            if (string.IsNullOrWhiteSpace(job.Company))
                errors.Add("Company is required.");

            if (job.Salary <= 0)
                errors.Add("Salary must be positive.");

            if (errors.Count > 0)
            {
                context.Result = new BadRequestObjectResult(
                    "Title and Company are required. Salary must be positive.");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
