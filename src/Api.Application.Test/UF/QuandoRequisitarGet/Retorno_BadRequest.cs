using application.Controllers;
using Domain.Dtos.Uf;
using Domain.Interfaces.Services.Uf;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.UF.QuandoRequisitarGet
{
    public class Retorno_BadRequest
    {
        private UfsController _controller;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var result = await _controller.Get(Guid.NewGuid());
            Assert.True(result is BadRequestObjectResult);
        }

        public Retorno_BadRequest()
        {
            #region Mock
            _serviceMock = new Mock<IUfService>();

            _serviceMock.Setup(m => m.Get(It.IsAny<Guid>())).ReturnsAsync(
                new UfDto
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    Sigla = "SP"
                });
            #endregion

            _controller = new UfsController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");
        }
    }
}
