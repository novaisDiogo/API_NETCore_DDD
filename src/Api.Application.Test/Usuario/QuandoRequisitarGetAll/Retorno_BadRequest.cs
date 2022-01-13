using Api.Application.Controllers;
using Api.Domain.Interfaces.Services.User;
using Domain.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequisitarGetAll
{
    public class Retorno_BadRequest
    {
        private UsersController _controller;

        [Fact(DisplayName = "É possivel realizar o getAll.")]
        public async Task E_Possivel_Invocar_a_Controller_GetAll()
        {
            var serviceMock = new Mock<IUserService>();

            serviceMock.Setup(m => m.GetAll()).ReturnsAsync(
                    new List<UserDto>
                    {
                        new UserDto
                        {
                            Id = Guid.NewGuid(),
                            Name = Faker.Name.FullName(),
                            Email = Faker.Internet.Email()
                        },
                        new UserDto
                        {
                            Id = Guid.NewGuid(),
                            Name = Faker.Name.FullName(),
                            Email = Faker.Internet.Email()
                        }
                    }
            );

            _controller = new UsersController(serviceMock.Object);
            _controller.ModelState.AddModelError("Id", "Formato Inválido");

            var result = await _controller.GetAll();

            Assert.True(result is BadRequestObjectResult);
        }
    }
}
