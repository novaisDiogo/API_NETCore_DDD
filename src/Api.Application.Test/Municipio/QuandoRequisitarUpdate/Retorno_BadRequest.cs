﻿using application.Controllers;
using Domain.Dtos.Municipio;
using Domain.Interfaces.Services.Municipio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Municipio.QuandoRequisitarUpdate
{
    public class Retorno_BadRequest
    {
        private MunicipiosController _controller;
        private Mock<IMunicipioService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o update")]
        public async Task E_Possivel_Realizar_Update()
        {
            var municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Nome = "São Paulo",
                CodIBGE = 1
            };
            var result = await _controller.Put(municipioDtoUpdate);
            Assert.True(result is BadRequestResult);
        }
        public Retorno_BadRequest()
        {
            _serviceMock = new Mock<IMunicipioService>();
            _serviceMock.Setup(m => m.Put(It.IsAny<MunicipioDtoUpdate>())).ReturnsAsync(
                new MunicipioDtoUpdateResult
                {
                    Id = Guid.NewGuid(),
                    Nome = "São Paulo",
                    UpdateAt = DateTime.Now
                });
            _controller = new MunicipiosController(_serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "É um campo Obrigatório");
        }

    }
}
