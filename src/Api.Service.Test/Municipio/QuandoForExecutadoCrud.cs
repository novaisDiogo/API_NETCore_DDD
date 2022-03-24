using Domain.Entities;
using Domain.Interfaces.Services.Municipio;
using Domain.Repository;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Municipio
{
    public class QuandoForExecutadoCrud : MunicipioTests
    {
        private IMunicipioService _service;

        private Mock<IMunicipioRepository> _mockRepository;

        public QuandoForExecutadoCrud()
        {
            _mockRepository = new Mock<IMunicipioRepository>();
            _mockRepository.Setup(m => m.InsertAsync(It.IsAny<MunicipioEntity>())).ReturnsAsync(municipioEntity);
            _mockRepository.Setup(m => m.UpdateAsync(It.IsAny<MunicipioEntity>())).ReturnsAsync(municipioEntity);
            _mockRepository.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _mockRepository.Setup(m => m.SelectAsync(It.IsAny<Guid>())).ReturnsAsync(municipioEntity);
            _mockRepository.Setup(m => m.SelectAsync()).ReturnsAsync(municipioList);
            _mockRepository.Setup(m => m.GetCompleteById(It.IsAny<Guid>())).ReturnsAsync(municipioEntity);
            _mockRepository.Setup(m => m.GetCompleteByIBGE(It.IsAny<int>())).ReturnsAsync(municipioEntity);

            _service = new MunicipioService(_mockRepository.Object, Mapper);
        }
        [Fact(DisplayName = "É possivel executar o método create")]
        public async Task E_Possivel_Executar_Metodo_Create()
        {
            var result = await _service.Post(municipioDtoCreate);

            Assert.NotNull(result);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(IdUf, result.UfId);
        }

        [Fact(DisplayName = "É possivel executar o método create")]
        public async Task E_Possivel_Executar_Metodo_Update()
        {
            var result = await _service.Put(municipioDtoUpdate);

            Assert.NotNull(result);
            Assert.Equal(IdMunicipio, result.Id);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.Equal(IdUf, result.UfId);
        }

        [Fact(DisplayName = "É possivel executar o método delete")]
        public async Task E_Possivel_Executar_Metodo_Delete()
        {
            var result = await _service.Delete(IdMunicipio);

            Assert.True(result);
        }

        [Fact(DisplayName = "É possivel executar o método delete false")]
        public async Task E_Possivel_Executar_Metodo_Delete_False()
        {
            _mockRepository.Setup(m => m.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            var result = await _service.Delete(IdMunicipio);

            Assert.False(result);
        }

        [Fact(DisplayName = "É possivel executar o método get")]
        public async Task E_Possivel_Executar_Metodo_Get()
        {
            var result = await _service.Get(IdMunicipio);

            Assert.NotNull(result);
            Assert.True(result.Id == IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
        }

        [Fact(DisplayName = "É possivel executar o método get null")]
        public async Task E_Possivel_Executar_Metodo_Get_Null()
        {
            _mockRepository.Setup(m => m.SelectAsync(It.IsAny<Guid>())).ReturnsAsync((MunicipioEntity)null);

            var result = await _service.Get(IdMunicipio);

            Assert.Null(result);
        }

        [Fact(DisplayName = "É possivel executar o método getAll")]
        public async Task E_Possivel_Executar_Metodo_GetAll()
        {
            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.True(result.Count() == 10);
        }

        [Fact(DisplayName = "É possivel executar o método getAll empty")]
        public async Task E_Possivel_Executar_Metodo_GetAll_Empty()
        {
            var listEmpty = new List<MunicipioEntity>();
            _mockRepository.Setup(m => m.SelectAsync()).ReturnsAsync(listEmpty.AsEnumerable);
            var result = await _service.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.True(result.Count() == 0);
        }

        [Fact(DisplayName = "É possivel executar o método get complete by id")]
        public async Task E_Possivel_Executar_Metodo_GetCompletById()
        {
            var result = await _service.GetCompleteById(IdMunicipio);

            Assert.NotNull(result);
            Assert.Equal(result.Id, IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.NotNull(result.Uf);
        }

        [Fact(DisplayName = "É possivel executar o método get complete by IBGE")]
        public async Task E_Possivel_Executar_Metodo_GetCompletBy_IBGE()
        {
            var result = await _service.GetCompleteByIBGE(CodigoIBGEMunicipio);

            Assert.NotNull(result);
            Assert.Equal(result.Id, IdMunicipio);
            Assert.Equal(NomeMunicipio, result.Nome);
            Assert.Equal(CodigoIBGEMunicipio, result.CodIBGE);
            Assert.NotNull(result.Uf);
        }
    }
}
