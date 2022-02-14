using Domain.Dtos.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        [Fact]
        public async Task E_possivel_realizar_crud_usuario()
        {
            await AddToken();

            _name = Faker.Name.First();
            _email = Faker.Internet.Email();

            var userDto = new UserDtoCreate()
            {
                Name = _name,
                Email = _email
            };

            var response = await PostJsonAsync(userDto, $"{hostApi}users", client);
            var postContent = await response.Content.ReadAsStringAsync();
            var postResult = JsonConvert.DeserializeObject<UserDtoCreateResult>(postContent);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(_name, postResult.Name);
            Assert.Equal(_email, postResult.Email);
            Assert.True(postResult.Id != default(Guid));
        }
    }
}
