using Newtonsoft.Json;
using System.Net.Http.Headers;
using Xunit;

namespace StarWars.Tests.IntegrationTest
{
    public class StarWarsIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public StarWarsIntegrationTests(TestingWebAppFactory<Program> factory)
            => _httpClient = factory.CreateClient();

        [Fact]
        public async Task Authentication_Route_Returns_Sucess()
        {
            // arrange
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                username = "admin",
                password = "admin"
            }));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var response = await _httpClient.PostAsync("/api/authentication", data);
            var stringResult = await response.Content.ReadAsStringAsync();

            // assert
            Assert.NotNull(stringResult);
        }

        [Fact]
        public async Task Authentication_Route_Returns_Invalid()
        {
            // arrange
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                username = "admin",
                password = ""
            }));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var response = await _httpClient.PostAsync("/api/authentication", data);
            var stringResult = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal("Invalid credentials", stringResult);
        }

        [Fact]
        public async Task Authentication_Route_Returns_BadRequest()
        {
            // arrange
            var data = new StringContent(JsonConvert.SerializeObject(new {}));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var response = await _httpClient.PostAsync("/api/authentication", data);
            var stringResult = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Equal("", stringResult);
        }

        [Fact]
        public async Task StarShip_Returns_Empty()
        {
            // act
            var response = await _httpClient.GetAsync("/api/starship");
            var stringResult = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Empty(stringResult);
        }

        [Fact]
        public async Task StarShip_Returns_Data()
        {
            // arrange
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                username = "admin",
                password = "admin"
            }));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var responseAuth = await _httpClient.PostAsync("/api/authentication", data);
            var stringResultAuth = await responseAuth.Content.ReadAsStringAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stringResultAuth);

            var responseStarship = await _httpClient.GetAsync("/api/starship");
            var stringResultStarship = await responseStarship.Content.ReadAsStringAsync();

            // assert
            Assert.NotEmpty(stringResultStarship);
        }

        [Fact]
        public async Task StarShip_Returns_With_Filter()
        {
            // arrange
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                username = "admin",
                password = "admin"
            }));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            // act
            var responseAuth = await _httpClient.PostAsync("/api/authentication", data);
            var stringResultAuth = await responseAuth.Content.ReadAsStringAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stringResultAuth);

            var responseStarship = await _httpClient.GetAsync("/api/starship?Manufacturer=Kuat Drive Yards");
            var stringResultStarship = await responseStarship.Content.ReadAsStringAsync();

            // assert
            Assert.NotEmpty(stringResultStarship);
        }

        [Fact]
        public async Task Manufacturer_Returns_Empty()
        {
            // act
            var response = await _httpClient.GetAsync("/api/manufacturer");
            var stringResult = await response.Content.ReadAsStringAsync();

            // assert
            Assert.Empty(stringResult);
        }

        [Fact]
        public async Task Manufacturer_Returns_Data()
        {
            // arrange
            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                username = "admin",
                password = "admin"
            }));

            data.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // act
            var responseAuth = await _httpClient.PostAsync("/api/authentication", data);
            var stringResultAuth = await responseAuth.Content.ReadAsStringAsync();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", stringResultAuth);

            var responseStarship = await _httpClient.GetAsync("/api/manufacturer");
            var stringResultStarship = await responseStarship.Content.ReadAsStringAsync();

            // assert
            Assert.NotEmpty(stringResultStarship);
        }

    }
}
