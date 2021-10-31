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
        public async Task<IActionResult> PostAsync(TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state!");
            }

            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance);
            await dbContext.AddAsync(item);
            await dbContext.SaveChangesAsync();

            return Ok(fields);
        }
    }
}
