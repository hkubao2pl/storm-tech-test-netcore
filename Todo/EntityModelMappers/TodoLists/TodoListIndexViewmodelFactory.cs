using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListIndexViewmodelFactory
    {
        public static async Task<TodoListIndexViewmodel> CreateAsync(IEnumerable<TodoList> todoLists)
        {
            List<TodoListSummaryViewmodel> lists = new List<TodoListSummaryViewmodel>();
            foreach (var item in todoLists)
            {
                lists.Add(await TodoListSummaryViewmodelFactory.CreateAsync(item));
            }
            return new TodoListIndexViewmodel(lists);
        }
    }
}