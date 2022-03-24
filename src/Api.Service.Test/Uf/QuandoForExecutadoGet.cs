using AutoMapper;
using Data.Implementations;
using Domain.Entities;
using Domain.Interfaces.Services.Uf;
using Domain.Repository;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Uf
{
    public class QuandoForExecutadoGet : UfTests
    {
        private IUfService _ufService;
        private IUfRepository _ufRepository;

        private Mock<IUfRepository> _ufRepositoryMock;

        public QuandoForExecutadoGet()
        {
            _ufRepositoryMock = new Mock<IUfRepository>();
            _ufRepositoryMock.Setup(m => m.SelectAsync(It.IsAny<Guid>())).ReturnsAsync(ufEntity);
            _ufRepositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync(UfDtoList);

            _ufRepository = _ufRepositoryMock.Object;

            _ufService = new UfService(_ufRepository, Mapper);
        }

        [Fact(DisplayName = "É possivel executar o método GET.")]
        public async Task E_Possivel_Executar_Metodo_GET()
        {
            var result = await _ufService.Get(IdUf);

            Assert.NotNull(result);
            Assert.True(result.Id == IdUf);
            Assert.Equal(result.Nome, Nome);
        }

        [Fact(DisplayName = "É possivel executar o método GETALL.")]
        public async Task E_Possivel_Executar_Metodo_GETALL()
        {
            var result = await _ufService.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "É possivel executar o método GETALL Empty.")]
        public async Task E_Possivel_Executar_Metodo_GETALL_Empty()
        {
            var listEmpty = new List<UfEntity>();
            _ufRepositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync(listEmpty.AsEnumerable);

            var resultEmpty = await _ufService.GetAll();

            Assert.Empty(resultEmpty);
            Assert.True(resultEmpty.Count() == 0);
        }
    }
}
