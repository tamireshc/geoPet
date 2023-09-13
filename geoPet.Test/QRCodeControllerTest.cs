using FluentAssertions;
using geoPet.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geoPet.Test
{
    public class QRCodeControllerTest : IClassFixture<FactoryTest<Program>>
    {

        private readonly HttpClient _client;
        private readonly FactoryTest<Program> _factory;

        public QRCodeControllerTest(FactoryTest<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetQRCodeForOwer()
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

                var result2 = await _client.GetAsync("/QRCode/Ower/1");
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }
        }

        [Fact]
        public async Task GetQRCodeForNonExistentOwer()
        {
            var result = await _client.GetAsync("/QRCode/Ower/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;

            stringResult.Should().Contain("Ower not found");
        }

        [Fact]
        public async Task GetQRCodeForPet()
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

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                var result3 = await _client.GetAsync("/QRCode/Pet/1");
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }

        }

        [Fact]
        public async Task GetQRCodeForNonExistentPet()
        {
            var result = await _client.GetAsync("/QRCode/Pet/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Pet not found");
        }
    }
}
