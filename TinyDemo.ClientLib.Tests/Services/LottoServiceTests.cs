using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using TinyDemo.ClientLib.Services;
using TinyDemo.SharedLib.Entities;
using Xunit;

namespace TinyDemo.ClientLib.Tests.Services
{
    public class LottoServiceTests
    {
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        [Fact]
        public async Task GenerateLotto_WhenApiReturnsSuccess_ShouldReturnLottoWithNumbers()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(new Lotto
                {
                    Numbers = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7 },
                    GeneratedOnTime = DateTime.Now
                }, _jsonOptions), Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var lottoService = new LottoService(httpClient);

            // Act
            var result = await lottoService.GenerateLotto();

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Numbers);
            Assert.Equal(7, result.Numbers.Count);
            Assert.Equal(1, result.Numbers[0]);
            Assert.Equal(2, result.Numbers[1]);
            Assert.Equal(3, result.Numbers[2]);
            Assert.Equal(4, result.Numbers[3]);
            Assert.Equal(5, result.Numbers[4]);
            Assert.Equal(6, result.Numbers[5]);
            Assert.Equal(7, result.Numbers[6]);
            Assert.NotEqual(default(DateTime), result.GeneratedOnTime);
        }

        [Fact]
        public async Task GenerateLotto_WhenApiReturnsEmptyBody_ShouldThrowException()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var lottoService = new LottoService(httpClient);

            // Act & Assert
            // Empty body should throw an exception (either JsonException or custom Exception)
            await Assert.ThrowsAnyAsync<Exception>(() => lottoService.GenerateLotto());
        }

        [Fact]
        public async Task GenerateLotto_WhenApiReturnsErrorStatus_ShouldThrowException()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent("Server error", Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var lottoService = new LottoService(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => lottoService.GenerateLotto());
        }

        [Fact]
        public async Task GenerateLotto_WhenApiReturnsInvalidJson_ShouldThrowException()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("invalid json", Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var lottoService = new LottoService(httpClient);

            // Act & Assert
            // Invalid JSON should throw a JsonException
            await Assert.ThrowsAnyAsync<Exception>(() => lottoService.GenerateLotto());
        }

        [Fact]
        public void LottoService_WhenInitialized_ShouldHaveCorrectApiUrl()
        {
            // Arrange
            var httpClient = new HttpClient();
            var lottoService = new LottoService(httpClient);

            // Act
            // Use reflection to access the private field for testing
            var apiUrlField = typeof(LottoService).GetField("_apiUrl", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var apiUrl = apiUrlField?.GetValue(lottoService) as string;

            // Assert
            Assert.NotNull(apiUrl);
            Assert.Equal("https://localhost:7049/api/Lotto", apiUrl);
        }

        [Fact]
        public void LottoService_WhenInitialized_ShouldHaveCorrectJsonOptions()
        {
            // Arrange
            var httpClient = new HttpClient();
            var lottoService = new LottoService(httpClient);

            // Act
            var optionsField = typeof(LottoService).GetField("_options", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var options = optionsField?.GetValue(lottoService) as JsonSerializerOptions;

            // Assert
            Assert.NotNull(options);
            Assert.Equal(JsonNamingPolicy.CamelCase, options.PropertyNamingPolicy);
        }

        [Fact]
        public async Task GenerateLotto_WhenApiReturnsNullResponse_ShouldThrowException()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = null
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var lottoService = new LottoService(httpClient);

            // Act & Assert
            // Null response should throw an exception (either from ReadAsStringAsync or custom exception)
            await Assert.ThrowsAnyAsync<Exception>(() => lottoService.GenerateLotto());
        }

        [Fact]
        public async Task GenerateLotto_WithValidResponse_ShouldDeserializeCorrectly()
        {
            // Arrange
            var testDateTime = new DateTime(2024, 1, 15, 10, 30, 0);
            var testNumbers = new ObservableCollection<int> { 5, 12, 19, 26, 33, 40, 45 };
            
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(new Lotto
                {
                    Numbers = testNumbers,
                    GeneratedOnTime = testDateTime
                }, _jsonOptions), Encoding.UTF8, "application/json")
            };

            mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var lottoService = new LottoService(httpClient);

            // Act
            var result = await lottoService.GenerateLotto();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(7, result.Numbers.Count);
            Assert.Equal(5, result.Numbers[0]);
            Assert.Equal(12, result.Numbers[1]);
            Assert.Equal(19, result.Numbers[2]);
            Assert.Equal(26, result.Numbers[3]);
            Assert.Equal(33, result.Numbers[4]);
            Assert.Equal(40, result.Numbers[5]);
            Assert.Equal(45, result.Numbers[6]);
            Assert.Equal(testDateTime, result.GeneratedOnTime);
        }
    }
}
