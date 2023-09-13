using FluentAssertions;
using geoPet.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


namespace geoPet.Test
{
    public class PositionControllerTest : IClassFixture<FactoryTest<Program>>
    {
        private readonly HttpClient _client;
        private readonly FactoryTest<Program> _factory;

        public PositionControllerTest(FactoryTest<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreatePosition()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }

        }

        [Fact]
        public async Task CreatePositionWithNonExistentPetId()
        {
            //add position
            var jsonToAdd = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":99}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/Position", stringContent);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);

            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Pet not found");
        }

        [Fact]
        public async Task GetAllPositions()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get all
                var result4 = await _client.GetAsync("/Position");
                result4.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result4.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("-19.8500207");
                stringResult.Should().Contain("-43.9453333");
                stringResult.Should().Contain("1");
            }
        }

        [Fact]
        public async Task GetPositionById()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get position by id
                var result4 = await _client.GetAsync("/Position/1");
                result4.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result4.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("-19.8500207");
                stringResult.Should().Contain("-43.9453333");
                stringResult.Should().Contain("1");
            }
        }

        [Fact]
        public async Task GetPositionByNonExistentId()
        {
            var result = await _client.GetAsync("/Position/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Position not found");
        }

        [Fact]
        public async Task DeletePosition()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get delele position by id
                var result4 = await _client.DeleteAsync("/Position/1");
                result4.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }
        }

        [Fact]
        public async Task DeletePositionWithAInexistentId()
        {
            var result = await _client.DeleteAsync("/Position/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Position not found");

        }

        [Fact]
        public async Task UpdatePosition()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get position by id
                var result4 = await _client.GetAsync("/Position/1");
                result4.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result4.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("-19.8500207");
                stringResult.Should().Contain("-43.9453333");
                stringResult.Should().Contain("1");

                //update position by id
                var jsonToAdd5 = "{\"latitude\":\"-20.850020\",\"longitude\":\"-45.9453333\",\"dateTime\":\"2023-09-06T22:25:40.477Z\",\"petId\":1}";
                var stringContent5 = new StringContent(jsonToAdd5, Encoding.UTF8, "application/json");
                var result5 = await _client.PutAsync("/Position/1", stringContent5);
                result5.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get position by id
                var result6 = await _client.GetAsync("/Position/1");
                result6.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult2 = result6.Content.ReadAsStringAsync().Result;
                stringResult2.Should().Contain("-20.850020");
                stringResult2.Should().Contain("-45.9453333");
                stringResult2.Should().Contain("1");
            }
        }

        [Fact]
        public async Task UpdatePositionWithNonExistentId()
        {
            var jsonToAdd = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync("/Position/99", stringContent);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);

            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Position not found");
        }

        [Fact]
        public async Task UpdatePositionWithNonExistentPetId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //update position by non existent pet id
                var jsonToAdd5 = "{\"latitude\":\"-20.850020\",\"longitude\":\"-45.9453333\",\"dateTime\":\"2023-09-06T22:25:40.477Z\",\"petId\":99}";
                var stringContent5 = new StringContent(jsonToAdd5, Encoding.UTF8, "application/json");
                var result5 = await _client.PutAsync("/Position/1", stringContent5);
                result5.StatusCode.Should().Be((System.Net.HttpStatusCode)404);

                var stringResult = result5.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("Pet not found");
            }
        }

        [Fact]
        public async Task GetLastPositionByPetId()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet 
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position 1
                var jsonToAdd3 = "{\"latitude\":\"-19.8500207\",\"longitude\":\"-43.9453333\",\"petId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PostAsync("/Position", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add position 2
                var jsonToAdd4 = "{\"latitude\":\"-20.8500207\",\"longitude\":\"-45.9453333\",\"petId\":1}";
                var stringContent4 = new StringContent(jsonToAdd4, Encoding.UTF8, "application/json");
                var result4 = await _client.PostAsync("/Position", stringContent4);
                result4.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get last Postion by pet id
                var result5 = await _client.GetAsync("/Position/Pet/1");
                result5.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result5.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("-20.8500207");
                stringResult.Should().Contain("-45.9453333");
            }
        }

        [Fact]
        public async Task GetLastPositionByNonExistentPetId()
        {
            var result = await _client.GetAsync("/Position/Pet/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Pet not found");
        }

        [Fact]
        public async Task GetLastPositionByPetWithoutPosition()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();

                //add  ower
                var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
                var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
                var result = await _client.PostAsync("/Ower", stringContent);
                result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //add  pet 
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);
                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

                //get last Postion by pet id
                var result3 = await _client.GetAsync("/Position/Pet/1");
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
                var stringResult = result3.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("position for this pet");
            }
        }
    }
}
