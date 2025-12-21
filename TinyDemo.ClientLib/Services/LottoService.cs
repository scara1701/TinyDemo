using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.ClientLib.Services
{
    public class LottoService : ILottoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            WriteIndented = false
        };

        public LottoService(HttpClient httpClient, string? apiUrl = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiUrl = apiUrl ?? "https://localhost:7049/api/Lotto";

            // Configure HttpClient for better performance
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<Lotto> GenerateLotto(CancellationToken cancellationToken = default)
        {
            try
            {
                // Use cancellation token for responsive cancellation
                var request = new HttpRequestMessage(HttpMethod.Get, _apiUrl);
                
                HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
                var lotto = await JsonSerializer.DeserializeAsync<Lotto>(responseStream, _options, cancellationToken).ConfigureAwait(false);

                if (lotto == null)
                    throw new InvalidOperationException("Empty or invalid Lotto data returned from server.");

                return lotto;
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Failed to generate lotto numbers from API.", ex);
            }
            catch (TaskCanceledException)
            {
                throw; // Re-throw cancellation exceptions
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while generating lotto numbers.", ex);
            }
        }
    }
}
