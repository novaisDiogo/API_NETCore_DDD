using Domain.Dtos.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.User
{
    public class QuandoRequisitarUsuario : BaseIntegration
    {
        private string _name { get; set; }
        private string _email { get; set; }

        public QuandoRequisitarUsuario()
        {
            _name = Faker.Name.First();
            _email = Faker.Internet.Email();
        }

        [Fact]
        public async Task E_possivel_realizar_crud_usuario()
        {
            await AddToken();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };

            //POST
            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var postContent = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<UserDtoCreateResult>(postContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, postResult.Name);
            Assert.Equal(_email, postResult.Email);
            Assert.True(postResult.Id != default(Guid));

            //GETALL
            response = await client.GetAsync($"{hostApi}users");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromJson = JsonConvert.DeserializeObject<IEnumerable<UserDto>>(jsonResult);
            Assert.True(listFromJson.Count() > 0);
            Assert.True(listFromJson.Where(c => c.Id == postResult.Id).Count() == 1);

            //PUT
            var updateUserDto = new UserDtoUpdate()
            {
                Id = postResult.Id,
                Name = Faker.Name.First(),
                Email = Faker.Internet.Email()
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateUserDto),
                Encoding.UTF8, "application/json");

            response = await client.PutAsync($"{hostApi}users", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var updatedResult = JsonConvert.DeserializeObject<UserDtoUpdateResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(postResult.Name, updatedResult.Name);
            Assert.NotEqual(postResult.Email, updatedResult.Email);

            //GETBYID

            response = await client.GetAsync($"{hostApi}users/{updatedResult.Id}");
            jsonResult = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<UserDto>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(getResult);
            Assert.Equal(getResult.Name, updatedResult.Name);
            Assert.Equal(getResult.Email, updatedResult.Email);

            //DELETE

            response = await client.DeleteAsync($"{hostApi}users/{updatedResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //GET ID, CHECK DELETE
            response = await client.GetAsync($"{hostApi}users/{updatedResult.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
