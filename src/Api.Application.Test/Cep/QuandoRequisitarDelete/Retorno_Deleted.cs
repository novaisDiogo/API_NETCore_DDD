using application.Controllers;
using Domain.Interfaces.Services.Cep;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Cep.QuandoRequisitarDelete
{
    public class Retorno_Deleted
    {
        private CepsController _controller;
        private Mock<ICepService> _serviceMock;

        [Fact(DisplayName = "É possivel realizar o Delete")]
        public async Task E_Possivel_Realizar_Delete()
        {
            var result = await _controller.Delete(Guid.NewGuid());
            Assert.True(result is OkObjectResult);
        }
        public Retorno_Deleted()
        {
            _serviceMock = new Mock<ICepService>();
            _serviceMock.Setup(m => m.Delete(It.IsAny<Guid>())).ReturnsAsync(true);

            _controller = new CepsController(_serviceMock.Object);
        }
    }
}
