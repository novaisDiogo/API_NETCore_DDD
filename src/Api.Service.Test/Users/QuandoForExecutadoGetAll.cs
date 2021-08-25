using Api.Domain.Interfaces.Services.User;
using Domain.Dtos.User;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Users
{
    public class QuandoForExecutadoGetAll : UserTests
    {
        private IUserService _service;
        private Mock<IUserService> _serviceMock;

        [Fact(DisplayName = "É possivel executar o método GET.")]
        public async Task E_Possivel_Executar_Metodo_GetAll()
        {
            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(listUserDto);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);

            var _listResult = new List<UserDto>();

            _serviceMock = new Mock<IUserService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(_listResult.AsEnumerable);
            _service = _serviceMock.Object;

            var _resultEmpty = await _service.GetAll();
            Assert.Empty(_resultEmpty);
            Assert.True(_resultEmpty.Count() == 0);
        }
    }
}
