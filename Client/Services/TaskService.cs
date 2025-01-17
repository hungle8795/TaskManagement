using System.Net.Http.Json;
using Client.Models;
using Client.Pages.TaskManagement;

namespace Client.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;
        private readonly ILogger<TaskService> _logger;

        public TaskService(HttpClient http, ILogger<TaskService> logger)
        {
            _http = http;
            _logger = logger;
        }

        public async Task<List<Models.TaskDetail>> GetTasksAsync()
        {
            _logger.LogInformation("API からすべてのタスクを取得しています...");
            try
            {
                var tasks = await _http.GetFromJsonAsync<List<Models.TaskDetail>>("taskdetail");
                _logger.LogInformation("合計 {TaskCount} 件のタスクを取得しました。", tasks?.Count ?? 0);
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "API からタスクを取得中にエラーが発生しました。");
                return new List<Models.TaskDetail>();
            }
        }

        public async Task<Models.TaskDetail> GetTaskByIdAsync(int id) {
            _logger.LogInformation("ID {TaskId} のタスクを取得しています...", id);
            try
            {
                var task = await _http.GetFromJsonAsync<Models.TaskDetail>($"taskdetail/{id}");
                if (task == null)
                {
                    _logger.LogWarning("ID {TaskId} のタスクが見つかりませんでした。", id);
                }
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {TaskId} のタスクを取得中にエラーが発生しました。", id);
                return null;
            }
        }

        public async Task<bool> CreateTaskAsync(Models.TaskDetail taskDetail)
        {
            _logger.LogInformation("新しいタスクを作成中: {TaskName} (担当者: {Assignee})", taskDetail.Name, taskDetail.Assignee);
            try
            {
                var response = await _http.PostAsJsonAsync("taskdetail", taskDetail);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("タスク {TaskName} を正常に作成しました。", taskDetail.Name);
                    return true;
                }
                else
                {
                    _logger.LogWarning("タスク {TaskName} の作成に失敗しました。ステータスコード: {StatusCode}", taskDetail.Name, response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "タスク {TaskName} の作成中にエラーが発生しました。", taskDetail.Name);
                return false;
            }
        }

        public async Task DeleteTaskAsync(int id) {
            _logger.LogInformation("ID {TaskId} のタスクを削除しています...", id);
            try
            {
                var response = await _http.DeleteAsync($"taskdetail/{id}");
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ID {TaskId} のタスクを正常に削除しました。", id);
                }
                else
                {
                    _logger.LogWarning("ID {TaskId} のタスクの削除に失敗しました。ステータスコード: {StatusCode}", id, response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {TaskId} のタスクの削除中にエラーが発生しました。", id);
            }
        }

        public async Task<bool> UpdateTaskAsync(int id, Models.TaskDetail taskDetail)
        {
            _logger.LogInformation("ID {TaskId} のタスクを更新しています...", id);
            try
            {
                var response = await _http.PutAsJsonAsync($"taskdetail/{id}", taskDetail);
                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("ID {TaskId} のタスクを正常に更新しました。", id);
                    return true;
                }
                else
                {
                    _logger.LogWarning("ID {TaskId} のタスクの更新に失敗しました。ステータスコード: {StatusCode}", id, response.StatusCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID {TaskId} のタスクの更新中にエラーが発生しました。", id);
                return false;
            }
        }

    }
}
