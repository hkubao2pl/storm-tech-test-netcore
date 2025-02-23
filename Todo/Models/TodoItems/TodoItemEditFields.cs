﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Todo.Data.Entities;

namespace Todo.Models.TodoItems
{
    public class TodoItemEditFields
    {
        public int TodoListId { get; set; }
        public string Title { get; set; }
        public string TodoListTitle { get; set; }
        public int TodoItemId { get; set; }
        public bool IsDone { get; set; }

        [DisplayName("Responsible party Id")]
        public string ResponsiblePartyId { get; set; }
        public Importance Importance { get; set; }

        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int Rank { get; set; }

        public TodoItemEditFields() { }

        public TodoItemEditFields(int todoListId, string todoListTitle, int todoItemId, string title, bool isDone, string responsiblePartyId, Importance importance, int rank)
        {
            TodoListId = todoListId;
            TodoListTitle = todoListTitle;
            TodoItemId = todoItemId;
            Title = title;
            IsDone = isDone;
            ResponsiblePartyId = responsiblePartyId;
            Importance = importance;
            Rank = rank;
        }
    }
}