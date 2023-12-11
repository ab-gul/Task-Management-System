using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tasks.API.Domain
{
    public class TaskItem : Base
    {
        public string Title { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public DateTime DueDate { get; private set; }
        public Status Status { get; private set; } = Status.Pending;

        public TaskItem(string title, string description, DateTime dueDate)
        {
            ArgumentNullException.ThrowIfNull(title, nameof(title));
            ArgumentNullException.ThrowIfNull(description, nameof(description));
            //TODO 
            // have an extension method to check for empty date time

            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public void Edit(string? title, string? description, Status? status)
        {
            this.Title = (title != null && this.Title != title) ? title : this.Title;
            this.Description = (description != null && this.Description != description) ? description : this.Description;
            this.Status = (status != null && this.Status != status) ? (Status)status : this.Status;
        }

    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
        {
            Pending = 0,
            InProgress = 1,
            Completed = 2,
            OverDue = 4
        }
}
