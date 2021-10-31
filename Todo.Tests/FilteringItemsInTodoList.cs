using System.Linq;
using Microsoft.AspNetCore.Identity;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoItems;
using Xunit;

namespace Todo.Tests
{
    public class FilteringItemsInTodoList
    {
        private readonly TodoList todoList;

        public FilteringItemsInTodoList()
        {
            string email = "alice @example.com";
            var user = new IdentityUser(email);
            user.Email = email;
            todoList = new TestTodoListBuilder(user, "shopping")
                    .WithItem("1", Importance.High, false, 1)
                    .WithItem("2", Importance.High, true, 2)
                    .WithItem("3", Importance.High, false, 4)
                    .WithItem("4", Importance.Medium, false, 1)
                    .WithItem("5", Importance.Medium, true, 3)
                    .WithItem("6", Importance.Low, false, 4)
                    .WithItem("7", Importance.Low, true, 5)
                    .WithItem("8", Importance.Low, false, 7)
                    .Build();
        }

        [Fact]
        public async void Case_HideDone_ImportanceOrder_Ascending()
        {
            var systemUnderTest = await TodoListDetailViewmodelFactory.CreateAsync(todoList, hideDone: true, importanceOrder: true, rankOrder: Order.Ascending);
            Assert.Equal("1", systemUnderTest.Items.First().Title);
            Assert.Equal("8", systemUnderTest.Items.Last().Title);
        }

        [Fact]
        public async void Case_HideDone_ImportanceOrder_Descending()
        {
            var systemUnderTest = await TodoListDetailViewmodelFactory.CreateAsync(todoList, hideDone: true, importanceOrder: true, rankOrder: Order.Descending);
            Assert.Equal("3", systemUnderTest.Items.First().Title);
            Assert.Equal("6", systemUnderTest.Items.Last().Title);
        }

        [Fact]
        public async void Case_HideDone_Ascending()
        {
            var systemUnderTest = await TodoListDetailViewmodelFactory.CreateAsync(todoList, hideDone: true, importanceOrder: false, rankOrder: Order.Ascending);
            Assert.Equal("1", systemUnderTest.Items.First().Title);
            Assert.Equal("8", systemUnderTest.Items.Last().Title);
        }

        [Fact]
        public async void Case_HideDone_Descending()
        {
            var systemUnderTest = await TodoListDetailViewmodelFactory.CreateAsync(todoList, hideDone: true, importanceOrder: false, rankOrder: Order.Descending);
            Assert.Equal("8", systemUnderTest.Items.First().Title);
            Assert.True("1" == systemUnderTest.Items.Last().Title || "4" == systemUnderTest.Items.Last().Title);
        }

        [Fact]
        public async void Case_ImportanceOrder_Ascending()
        {
            var systemUnderTest = await TodoListDetailViewmodelFactory.CreateAsync(todoList, hideDone: false, importanceOrder: true, rankOrder: Order.Ascending);
            Assert.True("1" == systemUnderTest.Items.First().Title || "4" == systemUnderTest.Items.First().Title);
            Assert.Equal("8", systemUnderTest.Items.Last().Title);
        }

        [Fact]
        public async void Case_ImportanceOrder_Descending()
        {
            var systemUnderTest = await TodoListDetailViewmodelFactory.CreateAsync(todoList, hideDone: false, importanceOrder: true, rankOrder: Order.Descending);
            Assert.Equal("3", systemUnderTest.Items.First().Title);
            Assert.Equal("6", systemUnderTest.Items.Last().Title);
        }
    }
}
