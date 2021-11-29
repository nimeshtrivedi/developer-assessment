using AutoMapper;
using TodoList.Models.Entities;
using TodoList.Models.ViewModels;

namespace TodoList.Services.MappingProfile
{
    public class ToDoItemMappingProfile :Profile
    {
        public ToDoItemMappingProfile()
        {
            CreateMap<TodoItem, TodoItemVm>().ReverseMap();
        }
    }
}
