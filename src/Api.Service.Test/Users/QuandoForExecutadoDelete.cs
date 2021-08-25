using Api.Domain.Interfaces.Services.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Users
{
    public class QuandoForExecutadoDelete : UserTests
    {
        private IUserService _service;

        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o Método Delete.")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(IdUsuario);

            Assert.True(result);
        }
    }
}
