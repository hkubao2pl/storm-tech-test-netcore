using System.Collections.Generic;
using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel Create(TodoList todoList, bool hideDone = false, bool importanceOrder = true, Order rankOrder = Order.Descending)
        {
            IEnumerable<TodoItem> items = todoList.Items;
            if (hideDone)
            {
                items = items.Where(i => !i.IsDone);
            }
            switch (rankOrder)
            {
                case Order.Ascending:
                    items = items.OrderBy(i => i.Rank);
                    break;
                case Order.Descending:
                    items = items.OrderByDescending(i => i.Rank);
                    break;
            }
            if(importanceOrder)
            {
                items = items.OrderBy(i => i.Importance);
            }

            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, hideDone, importanceOrder, rankOrder, items.Select(TodoItemSummaryViewmodelFactory.Create).ToList());
        }
    }
}