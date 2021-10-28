using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool hideDone = false)
        {
            IEnumerable<TodoItem> items = todoList.Items;
            if (hideDone)
            {
                items = items.Where(i => !i.IsDone);
            } 
                
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, hideDone, items.Select(TodoItemSummaryViewmodelFactory.Create).ToList());
        }
    }
}