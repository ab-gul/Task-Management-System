using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.ObjectModel;
using Tasks.WEB.Models;
using Tasks.WEB.Services;

namespace Tasks.WEB.Pages.TaskItem
{
    public class TaskListBase : ComponentBase
    {
        [Inject]
        protected ISnackbar Snackbar { get; set; }

        [Inject]
        protected ITaskService _tasksService { get; set; }

        [Inject]
        protected IDialogService _dialogService { get; set; }

        [Inject]
        NavigationManager _navigationManager { get; set; }

        protected ObservableCollection<TaskDTO> Tasks;

        protected bool _readOnly = true;

        protected override async Task OnInitializedAsync()
        {
            var response = await _tasksService.GetAllAsync();
            Tasks = new ObservableCollection<TaskDTO>(response);
        }

        protected void RowClickedAsync(DataGridRowClickEventArgs<TaskDTO> args)
        {
            //_navigationManager.NavigateTo($"/taskDetails/{args.Item.Id}");
        }

        protected async Task CommittedItemChanges(TaskDTO item)
        {
            await UpdateTaskAsync(item);
        }

        protected async Task DeleteButtonClickedAsync(TaskDTO item)
        {
            bool? result = await _dialogService.ShowMessageBox(
            "Warning",
            $"Sure about Deleting '{item.Title}' ?",
            yesText: "Delete",
            cancelText: "Cancel",
            options: new DialogOptions { FullWidth = true, MaxWidth = MaxWidth.ExtraSmall }); ;

            if (result is true)
            {
                await DeleteTaskAsync(item);
            }
        }

        private async Task DeleteTaskAsync(TaskDTO item)
        {
            await _tasksService.DeleteAsync(item.Id);

            Tasks.Remove(item);
            Snackbar.Add($"Task '{item.Title}' removed!", Severity.Info);

        }


        private async Task UpdateTaskAsync(TaskDTO item)
        {
            await _tasksService.UpdateAsync(item);

            Snackbar.Add("Task updated!", Severity.Success);

            var updatedResponse = await _tasksService.GetByIdAsync(item.Id);

            Tasks.Remove(item);
            Tasks.Insert(0, updatedResponse);
        }

        protected async Task AddTask()
        {
            //var parameters = new DialogParameters();
            //var dialogresult = _dialogService.Show<PostTaskForm>("New Task", parameters);

            //var result = await dialogresult.Result;

            //if (!result.Canceled)
            //{
            //    var response = await _tasksService.AddAsync((TaskDTO)result.Data);


            //    Tasks.Insert(0, response);
            //    Snackbar.Add($"Task with title: '{response.Title}' added!", Severity.Success);
            //}

            await Task.Delay(1000);

        }
    }
}