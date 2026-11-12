using System.Net.Http.Headers;
using Newtonsoft.Json;

using SchoolAppliction.Models;
namespace SchoolAppliction.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string _ApiURLPath = "https://localhost:7001/";

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_ApiURLPath);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<StudentEntity>> GetAllStudents(string AccessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            HttpResponseMessage response = await _httpClient.GetAsync("api/School/GetAllStudents");
            response.EnsureSuccessStatusCode();

            var contents= await response.Content.ReadAsStringAsync();
            var APIResponse=JsonConvert.DeserializeObject<List<StudentEntity>>(contents);
            return APIResponse;
        }

        public async Task AddStudent(StudentEntity student, string AccessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<StudentEntity>("api/School/AddStudent", student);
            response.EnsureSuccessStatusCode();
        }

        public async Task<StudentEntity> GetStudentDetailsById(int id, string AccessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            HttpResponseMessage response = await _httpClient.GetAsync($"api/School/GetStudent/" + id);

                response.EnsureSuccessStatusCode();

                var contents = await response.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<StudentEntity>(contents);
                return APIResponse;
            }

        public async Task UpdateStudent(int Id, StudentEntity student, string AccessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync<StudentEntity>("api/School/UpdateStudent", student);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteStudent(int Id, string AccessToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            HttpResponseMessage response = await _httpClient.DeleteAsync("api/School/DeleteStudent/" + Id);
            response.EnsureSuccessStatusCode();
        }
    }
}
