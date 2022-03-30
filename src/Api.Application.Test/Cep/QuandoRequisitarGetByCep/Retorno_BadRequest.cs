using application.Controllers;
using Domain.Dtos.Cep;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequisitarGetByCep
{
    public class Retorno_BadRequest
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o GetById")]
        public async Task E_Possivel_Realizar_GetById()
        {
            var result = await _controller.Get("0000000");
            Assert.True(result is BadRequestResult);
        }
        public Retorno_BadRequest()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<string>())).ReturnsAsync(new CepDto
            {
                Id = Guid.NewGuid(),
                Logradouro = "Teste de rua"
            });

            _controller = new CepsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Id é inválido");
        }
    }
}
