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

namespace Api.Application.Test.Cep.QuandoRequisitarCreate
{
    public class Retorno_BadRequest
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;
        private Mock<IUrlHelper> _urlMock;
        [Fact(DisplayName = "É possivel realizar o Created")]
        public async Task E_Possivel_Realizar_Created()
        {
            var cepDtoCreate = new CepDtoCreate { 
                Logradouro = "Teste de rua",
                Numero = "S/N"
            };

            var result = await _controller.Post(cepDtoCreate);
            Assert.True(result is BadRequestResult);
        }
        public Retorno_BadRequest()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Post(It.IsAny<CepDtoCreate>())).ReturnsAsync(
                new CepDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Logradouro = "Teste de rua",
                    CreateAt = DateTime.UtcNow
                });

            _controller = new CepsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Nome", "É um campo Obrigatório");

            _urlMock = new Mock<IUrlHelper>();
            _urlMock.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");
            _controller.Url = _urlMock.Object;
        }
    }
}
