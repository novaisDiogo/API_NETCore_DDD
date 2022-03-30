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

namespace Api.Application.Test.Municipio.QuandoRequisitarCreate
{
    public class Retorno_Created
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;
        private Mock<IUrlHelper> _urlMock;

        [Fact(DisplayName = "É possivel realizar o created")]
        public async Task E_Possivel_Realizar_Created()
        {
            var municipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = "São Paulo",
                CodIBGE = 1
            };
            var result = await _controller.Post(municipioDtoCreate);
            Assert.True(result is CreatedResult);
        }
        public Retorno_Created()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Post(It.IsAny<MunicipioDtoCreate>())).ReturnsAsync(
                new MunicipioDtoCreateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    CreateAt = DateTime.Now
                });

            _urlMock = new Mock<IUrlHelper>();
            _urlMock.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost:5000");

            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.Url = _urlMock.Object;
        }
    }
}
