using System;

namespace TodoList.Models.ViewModels
{
    public class TodoItemVm
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
