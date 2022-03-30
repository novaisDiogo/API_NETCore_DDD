using application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarGet
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestResult);
        }
        public Retorno_BadRequest()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new MunicipioDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo"
                });

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato inválido");
        }
    }
}
