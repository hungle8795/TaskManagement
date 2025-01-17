using System.Net.Http.Json;
using Client.Models;
using Client.Pages.TaskManagement;

namespace Client.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;

        public TaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Models.TaskDetail>> GetTasksAsync()
        {
            return await _http.GetFromJsonAsync<List<Models.TaskDetail>>("taskdetail"); //fetch
        }

        public async Task<Models.TaskDetail?> GetItemByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Models.TaskDetail>($"taskdetail/{id}");

        public async Task<bool> CreateTaskAsync(Models.TaskDetail taskDetail)
        {
            var response = await _http.PostAsJsonAsync("taskdetail/create", taskDetail);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteTaskAsync(int id) =>
            await _http.DeleteAsync($"taskdetail/{id}");
        public async Task<Models.TaskDetail> GetTaskByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Models.TaskDetail>($"taskdetail/{id}");
        public async Task<bool> UpdateTaskAsync(int id, Models.TaskDetail taskDetail)
        {
            var response = await _http.PutAsJsonAsync($"taskdetail/{id}", taskDetail);
            return response.IsSuccessStatusCode;
        }

    }
}
