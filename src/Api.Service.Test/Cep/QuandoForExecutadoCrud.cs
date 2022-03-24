using Domain.Entities;
using Domain.Interfaces.Services.Cep;
using Domain.Repository;
using Moq;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Service.Test.Cep
{
    public class QuandoForExecutadoCrud : CepTests
    {
        private Mock<ICepRepository> _mockCepRepository;

        private ICepService _cepService;

        public QuandoForExecutadoCrud()
        {
            _mockCepRepository = new Mock<ICepRepository>();
            _mockCepRepository.Setup(c => c.SelectAsync(It.IsAny<Guid>())).ReturnsAsync(cepEntity);
            _mockCepRepository.Setup(c => c.SelectAsync(It.IsAny<string>())).ReturnsAsync(cepEntity);
            _mockCepRepository.Setup(c => c.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            _mockCepRepository.Setup(c => c.InsertAsync(It.IsAny<CepEntity>())).ReturnsAsync(cepEntity);
            _mockCepRepository.Setup(c => c.UpdateAsync(It.IsAny<CepEntity>())).ReturnsAsync(cepEntity);

            _cepService = new CepService(_mockCepRepository.Object, Mapper);
        }

        [Fact(DisplayName = "Quando_For_Executado_Get_By_ID")]
        public async Task Quando_For_Executado_Get_By_ID()
        {
            var result = await _cepService.Get(IdCep);

            Assert.NotNull(result);
            Assert.NotNull(result.Municipio);
            Assert.NotNull(result.Municipio.Uf);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.MunicipioId, MunicipioId);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.Municipio.UfId, UfId);
        }

        [Fact(DisplayName = "Quando_For_Executado_Get_By_Cep")]
        public async Task Quando_For_Executado_Get_By_Cep()
        {
            var result = await _cepService.Get(Cep);

            Assert.NotNull(result);
            Assert.NotNull(result.Municipio);
            Assert.NotNull(result.Municipio.Uf);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.MunicipioId, MunicipioId);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.Municipio.UfId, UfId);
        }

        [Fact(DisplayName = "Quando_For_Executado_Delete_True")]
        public async Task Quando_For_Executado_Delete_True()
        {
            var result = await _cepService.Delete(IdCep);

            Assert.True(result);
        }

        [Fact(DisplayName = "Quando_For_Executado_Delete_False")]
        public async Task Quando_For_Executado_Delete_False()
        {
            _mockCepRepository.Setup(c => c.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            var result = await _cepService.Delete(IdCep);

            Assert.False(result);
        }

        [Fact(DisplayName = "Quando_For_Executado_Post")]
        public async Task Quando_For_Executado_Post()
        {
            var result = await _cepService.Post(cepDtoCreate);

            Assert.NotNull(result);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Cep, Cep);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.MunicipioId, MunicipioId);
        }

        [Fact(DisplayName = "Quando_For_Executado_Put")]
        public async Task Quando_For_Executado_Put()
        {
            var result = await _cepService.Put(CepDtoUpdate);

            Assert.NotNull(result);
            Assert.Equal(result.Id, IdCep);
            Assert.Equal(result.Cep, Cep);
            Assert.Equal(result.Logradouro, Logradouro);
            Assert.Equal(result.Numero, Numero);
            Assert.Equal(result.MunicipioId, MunicipioId);
        }
    }
}
