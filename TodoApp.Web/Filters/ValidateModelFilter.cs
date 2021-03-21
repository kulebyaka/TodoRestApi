using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoApp.Web.Filters
{
	/// <summary>
	/// Filter to check is a model valid
	/// </summary>
	public class ValidateModelFilter : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				context.Result = new BadRequestObjectResult(context.ModelState);
			}
		}
	}
}