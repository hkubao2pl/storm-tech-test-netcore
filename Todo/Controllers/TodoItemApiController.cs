using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemApiController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public TodoItemApiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(TodoItemApiCreateFields fields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state!");
            }
            Importance? importance = null;
            foreach (Importance i in (Importance[])Enum.GetValues(typeof(Importance)))
            {
                if(i.ToString() == fields.Importance)
                {
                    importance = i;
                    break;
                }
            }
            if(importance == null)
            {
                return NotFound();
            }
            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, importance.Value);
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return Ok(fields);
        }
    }
}
