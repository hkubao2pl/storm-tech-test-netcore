using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankApiController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RankApiController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RankDTO rank)
        {
            if (!ModelState.IsValid || rank?.Rank < 0 || rank.Rank > 10)
            {
                return BadRequest("Invalid model state!");
            }
            var todoItem = dbContext.SingleTodoItem(rank.TodoItemId);
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.Rank = rank.Rank;
            await dbContext.SaveChangesAsync();
            return Ok(rank);
        }
    }
}
