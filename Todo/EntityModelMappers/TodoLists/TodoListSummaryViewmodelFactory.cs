using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListSummaryViewmodelFactory
    {
        public static async Task<TodoListSummaryViewmodel> CreateAsync(TodoList todoList)
        {
            var numberOfNotDoneItems = todoList.Items.Count(ti => !ti.IsDone);
            var owner = await UserSummaryViewmodelFactory.CreateAsync(todoList.Owner);
            return new TodoListSummaryViewmodel(todoList.TodoListId, todoList.Title, numberOfNotDoneItems, owner);
        }
    }
}