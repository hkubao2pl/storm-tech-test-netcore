using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services;

namespace Todo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public RankController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post(RankDTO rank)
        {
            if (rank?.Rank < 0 || rank.Rank > 10)
            {
                return BadRequest("Rank value has to be between 0 and 10");
            }
            var todoItem = dbContext.SingleTodoItem(rank.TodoItemId);
            if (todoItem == null)
            {
                return BadRequest(@"TodoItemId {rank.TodoItemId} does not exist!");
            }
            todoItem.Rank = rank.Rank;
            await dbContext.SaveChangesAsync();
            return Ok(rank);
        }
    }
}
