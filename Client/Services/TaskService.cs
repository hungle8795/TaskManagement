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
                
                //var response = await _http.GetAsync("https://localhost:7126/api/Task");
                //response.EnsureSuccessStatusCode();
                //return await response.Content.ReadAsAsync<List<Models.Task>>();

                //var b = new List<Models.Task>();
                //var a = new Models.Task()
                //{
                //    Id = 2,
                //    Name = "Hung2",
                //    StartDate = DateTime.Now,
                //    CompletionDate = DateTime.Now,
                //    DueDate = DateTime.Now,
                //    Assignee = "game",
                //    Description = "check"
                //};
                //b.Add(a);
                //return b;
            }
            catch(Exception ex)
            {
                var t = ex.Message;
                return new List<Models.Task>();
            }
        }

        public async Task<Models.Task?> GetItemByIdAsync(int id) =>
            await _http.GetFromJsonAsync<Models.Task>($"task/{id}");

        public async System.Threading.Tasks.Task CreateTaskAsync(Models.Task task) =>
            await _http.PostAsJsonAsync("task", task);

        public async System.Threading.Tasks.Task UpdateTaskAsync(Models.Task task) =>
            await _http.PutAsJsonAsync($"task/{task.Id}", task);

        public async System.Threading.Tasks.Task DeleteTaskAsync(int id) =>
            await _http.DeleteAsync($"task/{id}");
    }
}
