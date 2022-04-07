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

namespace Api.Application.Test.Cep.QuandoRequisitarUpdate
{
    public class Retorno_Ok
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o Created")]
        public async Task E_Possivel_Realizar_Created()
        {
            var cepDtoUpdate = new CepDtoUpdate
            {
                Logradouro = "Teste de rua",
                Numero = "S/N"
            };

            var result = await _controller.Put(cepDtoUpdate);
            Assert.True(result is OkObjectResult);
        }
        public Retorno_Ok()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Put(It.IsAny<CepDtoUpdate>())).ReturnsAsync(
                new CepDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Logradouro = "Teste de rua",
                    UpdateAt = DateTime.UtcNow
                });

            _controller = new CepsController(_serviceMock.Object);
        }
    }
}
