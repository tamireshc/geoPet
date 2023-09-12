using FluentAssertions;
using geoPet.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace geoPet.Test
{
    public class OwerControllerTest : IClassFixture<FactoryTest<Program>>
    {
        private readonly HttpClient _client;
        private readonly FactoryTest<Program> _factory;

        public OwerControllerTest(FactoryTest<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task CreateOwer()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }

        }

        [Fact]
        public async Task CreateOwerWithNonExistentCEP()
        {
            var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"00000000\",\"password\":\"yuri\"}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/Ower", stringContent);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)400);

            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Nonexistent CEP)");
        }

        [Fact]
        public async Task CreateOwerWithIncorrectCEP()
        {
            var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"455898\",\"password\":\"yuri\"}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/Ower", stringContent);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)400);

            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Invalid CEP)");
        }


        [Fact]
        public async Task GetAllOwers()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add first ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add second ower
                var jsonToAdd2 = "{\"name\":\"Tamires\",\"email\":\"tamires@gmail.com\",\"cep\":\"31525380\",\"password\":\"tamires\"}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Ower", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //Get all owers
                var result3 = await _client.GetAsync("/Ower");
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result3.Content.ReadAsStringAsync().Result;

                stringResult.Should().Contain("Yuri");
                stringResult.Should().Contain("yuri@gmail.com");

                stringResult.Should().Contain("Tamires");
                stringResult.Should().Contain("tamires@gmail.com");
            }
        }

        [Fact]
        public async Task GetOwerbyId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //Get owers by id
                var result2 = await _client.GetAsync("/Ower/1");
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult2 = result2.Content.ReadAsStringAsync().Result;

                stringResult2.Should().Contain("Yuri");
                stringResult2.Should().Contain("yuri@gmail.com");
            }
        }

        [Fact]
        public async Task GetOwerbyNonExistentId()
        {
            var result = await _client.GetAsync("/Ower/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;

            stringResult.Should().Contain("Ower not found");
        }

        [Fact]
        public async Task DeleteOwer()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                //add ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //Delete owers by id
                var result2 = await _client.DeleteAsync("/Ower/1");
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }
        }

        [Fact]
        public async Task DeleteOwerWithNonExistentId()
        {
            var result = await _client.DeleteAsync("/Ower/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;

            stringResult.Should().Contain("Ower not found");
        }

        [Fact]
        public async Task UpdateOwer()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add a new ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //att this new ower
                var jsonToAdd2 = "{\"name\":\"Tamires\",\"email\":\"tamires@gmail.com\",\"cep\":\"31525380\",\"password\":\"tamires\"}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PutAsync("/Ower/1", stringContent2);

                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result2.Content.ReadAsStringAsync().Result;
            }

        }

        [Fact]
        public async Task UpdateOwerWithNonExistentOwer()
        {
            var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync("/Ower/99", stringContent);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);

            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Ower not found");
        }


        [Fact]
        public async Task UpdateOwerWithNonExistentCEP()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();


                //add a new ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //att this new ower
                var jsonToAdd2 = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"00000000\",\"password\":\"yuri\"}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PutAsync("/Ower/1", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)400);

                var stringResult = result2.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("Nonexistent CEP");
            }
        }

        [Fact]
        public async Task UpdateOwerWithIncorrectCEP()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();


                //add a new ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //att this new ower
                var jsonToAdd2 = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"000\",\"password\":\"yuri\"}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PutAsync("/Ower/1", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)400);

                var stringResult = result2.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("Invalid CEP");
            }

        }

        [Fact]
        public async Task LoginOwer()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add a new ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //Login
                var jsonToAdd2 = "{\"email\":\"yuri@gmail.com\",\"password\":\"yuri\"}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Login", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }

        }

        [Fact]
        public async Task LoginOwerWithIncorrectData()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add a new ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //Login
                var jsonToAdd2 = "{\"email\":\"yuri@gmail.com\",\"password\":\"yu\"}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Login", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
                var stringResult = result2.Content.ReadAsStringAsync().Result;

                stringResult.Should().Contain("Wrong user ou password");
            }

        }
    }
}
