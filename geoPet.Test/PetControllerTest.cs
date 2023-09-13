using geoPet.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;


namespace geoPet.Test
{
    public class PetControllerTest : IClassFixture<FactoryTest<Program>>
    {
        private readonly HttpClient _client;
        private readonly FactoryTest<Program> _factory;

        public PetControllerTest(FactoryTest<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task CreatePet()
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
            }

        }

        [Fact]
        public async Task CreatePetWithNonExistentOwer()
        {
            var jsonToAdd = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":99}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/Pet", stringContent);


            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Ower not found");
        }

        [Fact]
        public async Task CreateOwerWithIncorrectSize()
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

                //add pet
                var jsonToAdd2 = "{\"name\":\"JUJUBA\",\"age\":2,\"size\":\"XXXX\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
                var result2 = await _client.PostAsync("/Pet", stringContent2);


                var stringResult = result2.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("Invalid Size");
            }
        }


        [Fact]
        public async Task GetAllPets()
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

                //Get all pets
                var result3 = await _client.GetAsync("/Pet");
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result3.Content.ReadAsStringAsync().Result;

                stringResult.Should().Contain("JUJUBA");
                stringResult.Should().Contain("VIRA-LATA");
                stringResult.Should().Contain("1");
            }
        }

        [Fact]
        public async Task GetPetById()
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

                //Get pets by id
                var result3 = await _client.GetAsync("/Pet/1");
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result3.Content.ReadAsStringAsync().Result;

                stringResult.Should().Contain("JUJUBA");
                stringResult.Should().Contain("VIRA-LATA");
                stringResult.Should().Contain("1");
            }
        }

        [Fact]
        public async Task GetPetByNonExistentPetId()
        {
            var result = await _client.GetAsync("/Pet/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;

            stringResult.Should().Contain("Pet not found");
        }


        [Fact]
        public async Task DeletePet()
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

                //Delete Pet by id
                var result3 = await _client.DeleteAsync("/Pet/1");
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
            }
        }

        [Fact]
        public async Task DeletePetWithNonExistentId()
        {
            var result = await _client.DeleteAsync("/Pet/99");
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
            var stringResult = result.Content.ReadAsStringAsync().Result;

            stringResult.Should().Contain("Pet not found");
        }

        [Fact]
        public async Task UpdatePet()
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

                //att this new pet
                var jsonToAdd3 = "{\"name\":\"Damiao\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PutAsync("/Pet/1", stringContent3);

                result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
 
                //Get pets by id 1
                var result4 = await _client.GetAsync("/Pet/1");
                result4.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
                var stringResult = result4.Content.ReadAsStringAsync().Result;

                stringResult.Should().Contain("Damiao");
                stringResult.Should().Contain("VIRA-LATA");
                stringResult.Should().Contain("1");
            }

        }

        [Fact]
        public async Task UpdatePetWithNonExistentPetId()
        {

            var jsonToAdd = "{\"name\":\"Damiao\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":1}";
            var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
            var result = await _client.PutAsync("/Pet/99", stringContent);
            result.StatusCode.Should().Be((System.Net.HttpStatusCode)404);

            var stringResult = result.Content.ReadAsStringAsync().Result;
            stringResult.Should().Contain("Pet not found");
        }

        [Fact]
        public async Task UpdatePetWithNonExistentOwerId()
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

                //att pet with non existent ower id
                var jsonToAdd3 = "{\"name\":\"Damiao\",\"age\":2,\"size\":\"MEDIUM\",\"breed\":\"VIRA-LATA\",\"owerId\":99}";
                var stringContent3 = new StringContent(jsonToAdd3, Encoding.UTF8, "application/json");
                var result3 = await _client.PutAsync("/Pet/1", stringContent3);
                result3.StatusCode.Should().Be((System.Net.HttpStatusCode)404);

                var stringResult = result3.Content.ReadAsStringAsync().Result;
                stringResult.Should().Contain("Ower not found");
            }
        }


        //[Fact]
        //public async Task UpdateOwerWithNonExistentCEP()
        //{
        //    using (var scope = _factory.Services.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

        //        dbContext.Database.EnsureDeleted();
        //        dbContext.Database.EnsureCreated();


        //        //add a new ower
        //        var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
        //        var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
        //        var result = await _client.PostAsync("/Ower", stringContent);
        //        result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

        //        //att this new ower
        //        var jsonToAdd2 = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"00000000\",\"password\":\"yuri\"}";
        //        var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
        //        var result2 = await _client.PutAsync("/Ower/1", stringContent2);
        //        result2.StatusCode.Should().Be((System.Net.HttpStatusCode)400);

        //        var stringResult = result2.Content.ReadAsStringAsync().Result;
        //        stringResult.Should().Contain("Nonexistent CEP");
        //    }
        //}

        //[Fact]
        //public async Task UpdateOwerWithIncorrectCEP()
        //{
        //    using (var scope = _factory.Services.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

        //        dbContext.Database.EnsureDeleted();
        //        dbContext.Database.EnsureCreated();


        //        //add a new ower
        //        var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
        //        var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
        //        var result = await _client.PostAsync("/Ower", stringContent);
        //        result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

        //        //att this new ower
        //        var jsonToAdd2 = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"000\",\"password\":\"yuri\"}";
        //        var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
        //        var result2 = await _client.PutAsync("/Ower/1", stringContent2);
        //        result2.StatusCode.Should().Be((System.Net.HttpStatusCode)400);

        //        var stringResult = result2.Content.ReadAsStringAsync().Result;
        //        stringResult.Should().Contain("Invalid CEP");
        //    }

        //}

        //[Fact]
        //public async Task LoginOwer()
        //{
        //    using (var scope = _factory.Services.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

        //        dbContext.Database.EnsureDeleted();
        //        dbContext.Database.EnsureCreated();

        //        //add a new ower
        //        var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
        //        var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
        //        var result = await _client.PostAsync("/Ower", stringContent);
        //        result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

        //        //Login
        //        var jsonToAdd2 = "{\"email\":\"yuri@gmail.com\",\"password\":\"yuri\"}";
        //        var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
        //        var result2 = await _client.PostAsync("/Login", stringContent2);
        //        result2.StatusCode.Should().Be((System.Net.HttpStatusCode)200);
        //    }

        //}

        //[Fact]
        //public async Task LoginOwerWithIncorrectData()
        //{
        //    using (var scope = _factory.Services.CreateScope())
        //    {
        //        var dbContext = scope.ServiceProvider.GetRequiredService<GeoPetContext>();

        //        dbContext.Database.EnsureDeleted();
        //        dbContext.Database.EnsureCreated();

        //        //add a new ower
        //        var jsonToAdd = "{\"name\":\"Yuri\",\"email\":\"yuri@gmail.com\",\"cep\":\"31525380\",\"password\":\"yuri\"}";
        //        var stringContent = new StringContent(jsonToAdd, Encoding.UTF8, "application/json");
        //        var result = await _client.PostAsync("/Ower", stringContent);
        //        result.StatusCode.Should().Be((System.Net.HttpStatusCode)200);

        //        //Login
        //        var jsonToAdd2 = "{\"email\":\"yuri@gmail.com\",\"password\":\"yu\"}";
        //        var stringContent2 = new StringContent(jsonToAdd2, Encoding.UTF8, "application/json");
        //        var result2 = await _client.PostAsync("/Login", stringContent2);
        //        result2.StatusCode.Should().Be((System.Net.HttpStatusCode)404);
        //        var stringResult = result2.Content.ReadAsStringAsync().Result;

        //        stringResult.Should().Contain("Wrong user ou password");
        //    }

        //}
    }
}
