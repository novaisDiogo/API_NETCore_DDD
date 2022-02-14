using Domain.Dtos.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        }
    }
}
