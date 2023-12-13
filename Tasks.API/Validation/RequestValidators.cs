using FluentValidation;
using static Tasks.API.Contracts.V1.Tasks;

namespace Tasks.API.Validation
{
    public class Validators
    {
        public sealed class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
        {
            public CreateTaskRequestValidator()
            {
                RuleFor(c => c.description)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(" \'Description\' section can not be null, empty or whitespace");

                RuleFor(c => c.title)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(" \'Title\' section can not be null, empty or whitespace");

                RuleFor(c => c.dueDate)
                    .Cascade(CascadeMode.Stop)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage(" \'Due Date\' section can not be null, empty or whitespace")
                    .Must(c => c > DateTime.Now)
                    .WithMessage("\'Due Date\' section can not be past date");

            }
        }

        public sealed class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
        {
            public UpdateTaskRequestValidator()
            {

                RuleFor(c => c.title)
                    .NotEmpty()
                    .When(d => d.title != null)
                    .WithMessage("\'Title\' section can not be empty or whitespace");

                RuleFor(c => c.description)
                    .NotEmpty()
                    .When(d => d.description != null)
                    .WithMessage("\'Description\' section can not be empty or whitespace");
            }
        }

        public sealed class GetAllTasksRequestValidator : AbstractValidator<GetAllTasksRequest>
        {
            public GetAllTasksRequestValidator()
            {
                RuleFor(x => x.pageSize)
                    .Must(x => x > 0 && x <= 10)
                    .When(x => x.pageSize is not null)
                    .WithMessage("'Page size' must be between 1 and 10");

                RuleFor(x => x.pageNumber)
                    .GreaterThan(0)
                    .When(x => x.pageNumber is not null)
                    .WithMessage("\'Page number\' must be greater than 0");

            }
        }
    }
}
