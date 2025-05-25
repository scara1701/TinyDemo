using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.ClientLib.Services
{
    public class LottoService : ILottoService
    {
        private HttpClient _httpClient;
        private string _apiUrl;
        private JsonSerializerOptions _options;
        public LottoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiUrl = "https://localhost:7049/api/Lotto";

            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

        }
        public async Task<Lotto> GenerateLotto()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl);
                response.EnsureSuccessStatusCode();


                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody == null) throw new Exception("Empty body returned from server.");
                return JsonSerializer.Deserialize<Lotto>(responseBody, _options);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
