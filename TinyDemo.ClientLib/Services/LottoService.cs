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
            HttpResponseMessage response = await _httpClient.GetAsync(_apiUrl).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var lotto = await JsonSerializer.DeserializeAsync<Lotto>(responseStream, _options).ConfigureAwait(false);

            if (lotto == null)
                throw new Exception("Empty or invalid body returned from server.");

            return lotto;
        }
    }
}
