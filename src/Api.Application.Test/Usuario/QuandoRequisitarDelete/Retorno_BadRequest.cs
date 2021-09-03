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
    public class Retorno_BadRequest
    {
        private UsersController _controller;
        [Fact(DisplayName = "É possivel realizar o deleted")]
        public async Task E_Possivel_Invocar_A_Controller_Delete()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(false);

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");

            var guid = Guid.NewGuid();

            var result = await _controller.Delete(default(Guid));

            Assert.True(result is BadRequestObjectResult);
            Assert.True(!_controller.ModelState.IsValid);
        }
    }
}
