using FluentValidation;
using static Tasks.API.Contracts.V1.Tasks;

namespace Tasks.API.Validation
{
    public class Validators
    {
        private static bool IsValidGuid(Guid unValidatedGuid)
        {
            if (Guid.TryParse(unValidatedGuid.ToString(), out Guid validatedGuid))
            {
                return validatedGuid != Guid.Empty;
            }
            else
            {
                return false;
            }
        }

        private static bool IsValidGuid(Guid? unValidatedGuid)
        {
            if (Guid.TryParse(unValidatedGuid.ToString(), out Guid validatedGuid))
            {
                return validatedGuid != Guid.Empty;

            }
            else
            {
                return false;
            }
        }

        public sealed class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
        {
            public CreateTaskRequestValidator()
            {
                RuleFor(c => c.description)
                    .NotNull()
                    .WithMessage(" \'Description\' section can not be null")
                    .NotEmpty()
                    .WithMessage(" \'Description\' section can not be empty or whitespace");

                RuleFor(c => c.title)
                    .NotNull()
                    .WithMessage(" \'Title\' section can not be null")
                    .NotEmpty()
                    .WithMessage(" \'Title\' section can not be empty or whitespace");
                RuleFor(c => c.dueDate)
                    .NotNull()
                    .WithMessage(" \'Due Date\' section can not be null")
                    .NotEmpty()
                    .WithMessage(" \'Due Date\' section can not be empty or whitespace")
                    .Must(c => c.ToUniversalTime() > DateTime.UtcNow).WithMessage("\'Due Date\' section can not be past date");

            }
        }

        public sealed class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
        {
            public UpdateTaskRequestValidator()
            {

                RuleFor(c => c.title)
                    .NotEmpty().When(d => d.title != null)
                    .WithMessage("\'Title\' section can not be empty or whitespace");

                RuleFor(c => c.description)
                    .NotEmpty().When(d => d.description != null)
                    .WithMessage("\'Description\' section can not be empty or whitespace");
            }
        }
    }
}
