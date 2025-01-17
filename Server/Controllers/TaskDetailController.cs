using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TaskDetailController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TaskDetail>>> GetTaskDetails(){
            var taskDetails = await _context.TaskDetails.ToListAsync();
            return taskDetails;
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetail>> GetTaskById(int id)
        {
            var task = await _context.TaskDetails.FindAsync(id);
            return task != null ? Ok(task) : NotFound();
        }

        [HttpPost("create")]
        public async Task<ActionResult<TaskDetail>> CreateTask(TaskDetail taskDetail)
        {
            if(taskDetail == null)
            {
                return BadRequest();
            }
            _context.TaskDetails.Add(taskDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaskById), new { id = taskDetail.Id }, taskDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDetail taskDetail)
        {
            if (id != taskDetail.Id) return BadRequest();
            _context.Entry(taskDetail).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaskDetails.Any(e => e.Id == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskById(int id)
        {
            var task = await _context.TaskDetails.FindAsync(id);
            if (task == null) return NotFound();

            _context.TaskDetails.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

