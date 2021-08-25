using Api.Domain.Interfaces.Services.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Users
{
    public class QuandoForExecutadoCreate : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o Método Create.")]
        public async Task E_Possivel_Executar_Metodo_Create()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Post(userDtoCreate)).ReturnsAsync(dtoCreateResult);
            _service = _serviceMock.Object;

            var result = await _service.Post(userDtoCreate);

            Assert.NotNull(result);
            Assert.Equal(NomeUsuario, result.Name);
            Assert.Equal(EmailUsuario, result.Email);
        }
    }
}
