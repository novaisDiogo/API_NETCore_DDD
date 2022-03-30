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

namespace Api.Application.Test.Cep.QuandoRequisitarGet
{
    public class Retorno_Ok
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o Get")]
        public async Task E_Possivel_Realizar_Get()
        {
            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
        }
        public Retorno_Ok()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(new CepDto
            {
                Id = Guid.NewGuid(),
                Logradouro = "Teste de rua"
            });

            _controller = new CepsController(_serviceMock.Object);
        }
    }
}
