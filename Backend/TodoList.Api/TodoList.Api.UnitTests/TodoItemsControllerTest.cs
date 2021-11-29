using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Api.Controllers;
using TodoList.DataAccess.Repositories.Interfaces;
using TodoList.Models.ViewModels;
using TodoList.Services.Implementations;
using TodoList.Services.Interfaces;
using TodoList.Services.MappingProfile;
using TodoList.Models.Entities;
using System.Linq.Expressions;

namespace TodoList.Api.UnitTests
{
    [TestFixture]
    public class TodoItemsControllerTest
    {
        private TodoItemVm putTodoItem;
        
        private Mock<ITodoItemRepository> _mockRepo;
        
        private TodoService _service;
        private Mock<ILogger<TodoService>> _serviceLogger;
        private Mock<ILogger<TodoItemsController>> _controllerLogger;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<ToDoItemMappingProfile>());
            putTodoItem = new TodoItemVm { Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"), Description = "Testing", IsCompleted = false };
            _mockRepo = new Mock<ITodoItemRepository>();
            
            _serviceLogger = new Mock<ILogger<TodoService>>();
            _controllerLogger = new Mock<ILogger<TodoItemsController>>();
            _service = new TodoService(_mockRepo.Object, config.CreateMapper(), _serviceLogger.Object);
            _mockRepo.Setup(repo => repo.GetAll(null, null))
                .Returns(Task.FromResult<IEnumerable<TodoItem>>(new List<TodoItem> { new TodoItem { Id = new Guid(), Description = "Testing", IsCompleted = false } }));
            _mockRepo.Setup(repo => repo.Add(It.IsAny<TodoItem>()))
                .Returns(Task.FromResult(new TodoItem { Id = new Guid(), Description = "Testing", IsCompleted=false }));

            


        }
        [Test]
        public async Task PutTodoItem_WithDifferentIdFromPassedEntity_ShouldReturnBadRequest()
        {
            //arrange
            var controller = new TodoItemsController(_service, _controllerLogger.Object);
            //act
            var response = await controller.PutTodoItem(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa9"), putTodoItem);
            //assert
            Assert.IsInstanceOf<BadRequestResult>(response);
            
        }

        [Test]
        public async Task PostTodoItem_CalledWithEmptyDescription_ShouldReturnBadRequest()
        {
            // public async Task<IActionResult> PostTodoItem(TodoItemAddVm todoItem)
            //arrange
            var controller = new TodoItemsController(_service, _controllerLogger.Object);
            //act
            var response = await controller.PostTodoItem(new TodoItemAddVm());
            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Description is required", (response as ObjectResult).Value);
        }

        [Test]
        public async Task PostTodoItem_CalledWithExistingDescription_ShouldReturnBadRequest()
        {
            // public async Task<IActionResult> PostTodoItem(TodoItemAddVm todoItem)
            //arrange
            var controller = new TodoItemsController(_service, _controllerLogger.Object);
            //act
            var responseFirst = await controller.PostTodoItem(new TodoItemAddVm() { Description = "Testing" });
            var response = await controller.PostTodoItem(new TodoItemAddVm() { Description = "Testing"});
            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Description already exists", (response as ObjectResult).Value);
        }
    }
}
