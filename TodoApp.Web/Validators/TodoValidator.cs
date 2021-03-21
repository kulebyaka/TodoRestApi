using FluentValidation;
using TodoApp.Infrastructure.Models;
using TodoApp.Web.ViewModels;

namespace TodoApp.Web.Validators
{
	public class TodoValidator : AbstractValidator<TodoDTO>
	{
		public TodoValidator()
		{
			RuleFor(p => p.Title).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!");
		}
	}

	public class TodoViewModelValidator : AbstractValidator<TodoViewModel>
	{
		public TodoViewModelValidator()
		{
			RuleFor(p => p.Title).NotEmpty().WithMessage("{PropertyName} should be not empty. NEVER!");
		}
	}
}