using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet()]
        //public async Task<ActionResult<IEnumerable<Models.Task>>> GetTasks(){
        public async Task<List<Models.Task>> GetAll()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return tasks;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task != null ? Ok(task) : NotFound();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Models.Task>> CreateTask(Models.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, Models.Task task)
        {
            if (id != task.Id) return BadRequest();
            _context.Entry(task).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

