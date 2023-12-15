using FluentValidation;
using Tasks.WEB.Models;

namespace Tasks.WEB.Validators.TaskItems
{

    public class TaskPostValidator : AbstractValidator<TaskDTO>
    {
        public TaskPostValidator()
        {
            RuleFor(c => c.Title)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(" \'Title\' is required");

            RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage(" \'Description\' is required");

            //TODO can adjust calendar not to show the past date
            RuleFor(c => c.DueDate)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(" \'Due Date\' section can not be null, empty or whitespace")
                    .Must(c => c > DateTime.Now)
                    .WithMessage("\'Due Date\' section can not be past date");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<TaskDTO>.CreateWithOptions((TaskDTO)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}

