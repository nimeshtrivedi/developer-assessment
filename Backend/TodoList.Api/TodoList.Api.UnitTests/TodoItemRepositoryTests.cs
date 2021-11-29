using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.DataAccess;
using TodoList.DataAccess.Repositories.Implementation;
using TodoList.Models.Entities;
using TodoList.Models.ViewModels;

namespace TodoList.Api.UnitTests
{
    [TestFixture]
    public class TodoItemRepositoryTests
    {
        private TodoItem addItem1;
        private TodoItem addItem2;
        private TodoItem addItem3;
        DbContextOptions<TodoContext> options;
        public TodoItemRepositoryTests()
        {
            addItem1 = new TodoItem() { Description = "Task 1: Go for a walk" };
            addItem2 = new TodoItem() { Description = "Task 2: Prepare Breakfast" };
            addItem3 = new TodoItem() { Description = "Task 3: Attend Meeting at 10 am" };
        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TodoContext>().UseInMemoryDatabase(databaseName: "temp").Options;
        }

        [Test]
        public void GetAllToDos_AfterAdding3Tashs_CheckValuesBackFromDb()
        {
            // arrange
            var expectedResult = new List<TodoItem> { addItem1, addItem2, addItem3};
            
            using (var context = new TodoContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new TodoItemRepository(context);
                repository.Add(addItem1).GetAwaiter().GetResult();
                repository.Add(addItem2).GetAwaiter().GetResult();
                repository.Add(addItem3).GetAwaiter().GetResult();
            }
            // act
            IEnumerable<TodoItem> actualList;
            using (var context = new TodoContext(options))
            {
                var repository = new TodoItemRepository(context);
                actualList = repository.GetAll(null).GetAwaiter().GetResult();
            }


            // assert
            CollectionAssert.AreEqual(expectedResult, actualList, new TodoListCompare());
        }

        private class TodoListCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var item1 = (TodoItem)x;
                var item2 = (TodoItem)y;
                if (item1.Description != item2.Description)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
