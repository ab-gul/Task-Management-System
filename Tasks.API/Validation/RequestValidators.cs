using FluentValidation;
using System.Text.Json;
using Tasks.API.Domain;
using static Tasks.API.Contracts.V1.Tasks;

namespace Tasks.API.Validation
{
    public class Validators
    {
        private static bool IsValidTaskStatus(string status)
        {
            if (Enum.TryParse(typeof(Status), status, true, out var result))
            {
                return (Status)result != Status.OverDue;
            }

            return false;
        }

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

        public sealed class JsonValidator : AbstractValidator<JsonElement>
        {


            public JsonValidator()
            {
                RuleFor(model => model)
                     .Custom((model, context) =>
                     {
                         var updateTaskModel = JsonSerializer.Deserialize<UpdateTaskRequest>(model.GetRawText());

                         if (updateTaskModel == null)
                         {
                             context.AddFailure("Invalid JSON format.");
                             return;
                         }

                         var allowedProperties = typeof(UpdateTaskRequest).GetProperties().Select(p => p.Name);

                         var extraProperties = model
                             .EnumerateObject()
                             .Select(p => p.Name)
                             .Except(allowedProperties);

                         if (extraProperties.Any())
                         {
                             context.AddFailure($"The following properties are not allowed: {string.Join(", ", extraProperties)}");
                         }

                         var validationResult = new UpdateTaskRequestValidator().Validate(updateTaskModel);

                         if (!validationResult.IsValid)
                         {
                             validationResult.Errors.ForEach(error => context.AddFailure($"$.{error.PropertyName}", error.ErrorMessage));
                         }
                     });
                
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

                RuleFor(c => c.status)
                .Must(IsValidTaskStatus!)
                .When(d => d.status != null)
                .WithMessage("Given status is not allowed. Allowed statusses include : [" +
                $"{string.Join(", ", Enum.GetNames(typeof(Status)).Except(new string[] { Status.OverDue.ToString() }))}]");


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
