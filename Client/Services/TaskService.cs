using System.Net.Http.Json;
using Client.Models;

namespace Client.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;

        public TaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Models.Task>> GetTasksAsync()
        {
            try
            {
                return await _http.GetFromJsonAsync<List<Models.Task>>("Task"); //fetch
            }
            catch (Exception ex)
            {
                var t = ex.Message;
                return new List<Models.Task>();
            }
        }

        public async Task<Models.Task?> GetItemByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Models.Task>($"task/{id}");

        public async System.Threading.Tasks.Task<bool> CreateTaskAsync(Models.Task task)
        {
            var response = await _http.PostAsJsonAsync("task/create", task);
            return response.IsSuccessStatusCode;
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id) =>
            await _http.DeleteAsync($"task/{id}");
        public async Task<Models.Task> GetTaskByIdAsync(int id) => 
            await _http.GetFromJsonAsync<Models.Task>($"task/{id}");
        public async Task<bool> UpdateTaskAsync(int id, Models.Task task)
        {
            var response = await _http.PutAsJsonAsync($"task/{id}", task);
            return response.IsSuccessStatusCode;
        }

    }
}
