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

namespace Api.Application.Test.Municipio.QuandoRequisitarGetAll
{
    public class Retorno_Ok
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o Get")]
        public async Task E_Possivel_Invocar_a_Controller_Get()
        {
            var result = await _controller.GetAll();
            Assert.True(result is OkObjectResult);
        }
        public Retorno_Ok()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<MunicipioDto>
                {
                    new MunicipioDto
                    {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo"
                    },
                    new MunicipioDto
                    {
                    Id = Guid.NewGuid(),
                    Nome = "Amazonas"
                    }
                });

            _controller = new MunicipiosController(_serviceMock.Object);
        }
    }
}
