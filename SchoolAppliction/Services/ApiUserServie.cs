using Newtonsoft.Json;
using SchoolAppliction.Dto;
using System.Net.Http.Headers;

namespace SchoolAppliction.Services
{
    public class ApiUserServie
    {
        private readonly HttpClient _httpClient;

        public ApiUserServie()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7001/")
            };

            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<TokenResponseDto> AuthenticateUser(
            LoginUser userDetails)
        {
            HttpResponseMessage response =
                await _httpClient.PostAsJsonAsync(
                    "api/Auth/login",
                    userDetails);

           var contents = await response.Content.ReadAsStringAsync();
            var APIResponse = JsonConvert.DeserializeObject<TokenResponseDto>(contents);

            return APIResponse;
        }
    }
}
