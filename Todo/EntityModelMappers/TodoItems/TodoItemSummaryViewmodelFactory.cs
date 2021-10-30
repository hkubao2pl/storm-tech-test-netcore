using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.EntityModelMappers.TodoItems
{
    public static class TodoItemSummaryViewmodelFactory
    {
        public static async Task<TodoItemSummaryViewmodel> CreateAsync(TodoItem ti)
        {
            var responsibleParty = await UserSummaryViewmodelFactory.CreateAsync(ti.ResponsibleParty);
            return new TodoItemSummaryViewmodel(ti.TodoItemId, ti.Title, ti.IsDone, responsibleParty, ti.Importance, ti.Rank);
        }
    }
}