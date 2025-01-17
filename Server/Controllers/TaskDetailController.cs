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
        private readonly ILogger<TaskDetailController> _logger;

        public TaskDetailController(AppDbContext context, ILogger<TaskDetailController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<TaskDetail>>> GetAll()
        {
            //_logger.LogInformation("すべてのタスクを取得しています...");
            var taskDetails = await _context.TaskDetails.ToListAsync();
            //_logger.LogInformation("合計 {TaskCount} 件のタスクを取得しました。", taskDetails.Count);
            return taskDetails;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDetail>> GetById(int id)
        {
            _logger.LogInformation("ID {TaskId} のタスクを取得しています...", id);
            var task = await _context.TaskDetails.FindAsync(id);
            if (task == null)
            {
                _logger.LogWarning("ID {TaskId} のタスクが見つかりませんでした。", id);
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost()]
        public async Task<ActionResult<TaskDetail>> Create(TaskDetail taskDetail)
        {
            if (taskDetail == null)
            {
                _logger.LogWarning("タスク作成: null の taskDetail オブジェクトを受信しました。");
                return BadRequest();
            }
            _logger.LogInformation("新しいタスクを作成中: {TaskName} (担当者: {Assignee})", taskDetail.Name, taskDetail.Assignee);
            try
            {
                _context.TaskDetails.Add(taskDetail);
                await _context.SaveChangesAsync();
                _logger.LogInformation("タスク {TaskName} が正常に作成されました。ID: {TaskId}", taskDetail.Name, taskDetail.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "タスク {TaskName} の作成中にエラーが発生しました。", taskDetail.Name);
                return StatusCode(500, "サーバー内部エラー");
            }
            return CreatedAtAction(nameof(GetById), new { id = taskDetail.Id }, taskDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskDetail taskDetail)
        {
            if (id != taskDetail.Id)
            {
                _logger.LogWarning("タスク更新: ID {TaskId} とペイロード ID {PayloadId} が一致しません。", id, taskDetail.Id);
                return BadRequest();
            }
            _context.Entry(taskDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation("ID {TaskId} のタスクを正常に更新しました。", id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaskDetails.Any(e => e.Id == id))
                {
                    _logger.LogWarning("タスク更新: ID {TaskId} のタスクが見つかりませんでした。", id);
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            _logger.LogInformation("ID {TaskId} のタスクを削除しています...", id);
            var task = await _context.TaskDetails.FindAsync(id);
            if (task == null)
            {
                _logger.LogWarning("タスク削除: ID {TaskId} のタスクが見つかりませんでした。", id);
                return NotFound();
            }
            _context.TaskDetails.Remove(task);
            await _context.SaveChangesAsync();
            _logger.LogInformation("ID {TaskId} のタスクを正常に削除しました。", id);
            return NoContent();
        }
    }
}
