using Microsoft.AspNetCore.Components;
using MudBlazor;
using Tasks.WEB.Models;
using Tasks.WEB.Validators.TaskItems;

namespace Tasks.WEB.Pages.TaskItem
{
    public class TaskPostBase : ComponentBase
    {
        [CascadingParameter]
        protected MudDialogInstance MudDialog { get; set; }

        protected MudForm form;

        protected TaskPostValidator taskValidator = new();

        protected TaskDTO model = new();

        protected async Task Submit()
        {
            await form.Validate();

            if (form.IsValid)
            {
                model.DueDate = model.DueDate!.Value.AddHours(23).AddMinutes(59).AddSeconds(59);
                MudDialog.Close(DialogResult.Ok(model));
            }
        }

        protected void Cancel()
        {
            MudDialog.Cancel();
        }

    }
}