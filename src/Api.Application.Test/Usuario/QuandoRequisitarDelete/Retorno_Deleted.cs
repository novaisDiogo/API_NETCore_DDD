using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Domain.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarDelete
{
    public class Retorno_Deleted
    {
        private UsersController _controller;
        [Fact(DisplayName = "É possivel realizar o deleted")]
        public async Task E_Possivel_Invocar_A_Controller_Delete()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new UsersController(serviceMock.Object);

            var guid = Guid.NewGuid();

            var result = await _controller.Delete(guid);

            Assert.True(result is OkObjectResult);

            var resultValue = ((OkObjectResult)result).Value;
            Assert.NotNull(resultValue);
            Assert.True((bool)resultValue);
        }
    }
}
