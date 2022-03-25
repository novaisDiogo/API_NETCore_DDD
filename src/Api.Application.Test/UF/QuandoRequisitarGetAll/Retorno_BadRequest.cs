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

namespace Api.Application.Test.UF.QuandoRequisitarGetAll
{
    public class Retorno_badRequest
    {
        private UfsController _ufsController;
        private Mock<IUfService> _serviceMock;

        [Fact(DisplayName = "É possivel Realizar o GetAll")]
        public async Task E_Possivel_Invocar_a_Controller_GetALL()
        {
            var result = await _ufsController.GetAll();
            Assert.True(result is BadRequestObjectResult);
        }

        public Retorno_badRequest()
        {
            #region Mock
            _serviceMock = new Mock<IUfService>();
            _serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                new List<UfDto>
                {
                    new UfDto
                    {
                        Id = Guid.NewGuid(),
                        Nome = "São Paulo",
                        Sigla = "SP"
                    },
                    new UfDto
                    {
                        Id = Guid.NewGuid(),
                        Nome = "Amazonas",
                        Sigla = "AM"
                    }
                });
            #endregion

            _ufsController = new UfsController(_serviceMock.Object);
            _ufsController.ModelState.AddModelError("Id", "Formato Inválido");
        }
    }
}
