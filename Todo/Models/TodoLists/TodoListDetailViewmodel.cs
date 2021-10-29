using System.Collections.Generic;
using System.ComponentModel;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Models.TodoLists
{
    public class TodoListDetailViewmodel
    {
        public int TodoListId { get; }
        public string Title { get; }
        public bool HideDone { get; }
        public bool ImportanceOrder { get; }
        [DisplayName("Rank order")]
        public Order RankOrder { get; }
        public ICollection<TodoItemSummaryViewmodel> Items { get; }

        public TodoListDetailViewmodel(int todoListId, string title, bool hideDone, bool importanceOrder, Order rankOrder, ICollection<TodoItemSummaryViewmodel> items)
        {
            Items = items;
            TodoListId = todoListId;
            Title = title;
            HideDone = hideDone;
            ImportanceOrder = importanceOrder;
            RankOrder = rankOrder;
        }
    }
}