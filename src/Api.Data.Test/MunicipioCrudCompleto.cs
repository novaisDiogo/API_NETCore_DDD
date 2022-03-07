using Api.Data.Context;
using Data.Implementations;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class MunicipioCrudCompleto : BaseTest, IClassFixture<DbTest>
    {
        private ServiceProvider _serviceProvider;
        public MunicipioCrudCompleto(DbTest dbTest)
        {
            _serviceProvider = dbTest.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Municipio")]
        [Trait("CRUD", "MunicipioEntity")]
        public async Task E_Possivel_Realizar_CRUD_Municipio()
        {
            using(var context = _serviceProvider.GetService<MyContext>())
            {
                MunicipioImplementation _repositorio = new MunicipioImplementation(context);
                MunicipioEntity _entity = new MunicipioEntity
                {
                    Nome = Faker.Address.City(),
                    CodIBGE = Faker.RandomNumber.Next(1000000, 9999999),
                    UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
                };

                //CREATE
                var _registroCriado = await _repositorio.InsertAsync(_entity);
                Assert.NotNull(_registroCriado);
                Assert.Equal(_entity.Nome, _registroCriado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroCriado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroCriado.UfId);
                Assert.False(_registroCriado.Id == Guid.Empty);

                //UPDATE
                _entity.Nome = Faker.Address.City();
                _entity.Id = _registroCriado.Id;

                var _registroAtualizado = await _repositorio.UpdateAsync(_entity);
                Assert.NotNull(_registroAtualizado);
                Assert.Equal(_entity.Nome, _registroAtualizado.Nome);
                Assert.Equal(_entity.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_entity.UfId, _registroAtualizado.UfId);
                Assert.Equal(_entity.Id, _registroAtualizado.Id);

                //EXIST
                var _registroExiste = await _repositorio.ExistAsync(_registroAtualizado.Id);
                Assert.True(_registroExiste);

                //SELECT
                var _registroSelecionado = await _repositorio.SelectAsync(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.Equal(_registroSelecionado.Id, _registroAtualizado.Id);
                Assert.Null(_registroSelecionado.Uf);

                //GET_COMPLETE_BY_IBGE
                _registroSelecionado = await _repositorio.GetCompleteByIBGE(_registroAtualizado.CodIBGE);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.Equal(_registroSelecionado.Id, _registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado.Uf);

                //GET_COMPLETE_BY_ID
                _registroSelecionado = await _repositorio.GetCompleteById(_registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado);
                Assert.Equal(_registroSelecionado.Nome, _registroAtualizado.Nome);
                Assert.Equal(_registroSelecionado.CodIBGE, _registroAtualizado.CodIBGE);
                Assert.Equal(_registroSelecionado.UfId, _registroAtualizado.UfId);
                Assert.Equal(_registroSelecionado.Id, _registroAtualizado.Id);
                Assert.NotNull(_registroSelecionado.Uf);

                //GET_ALL
                var _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() > 0);

                //DELETE
                var _removeu = await _repositorio.DeleteAsync(_registroSelecionado.Id);
                Assert.True(_removeu);

                _todosRegistros = await _repositorio.SelectAsync();
                Assert.NotNull(_todosRegistros);
                Assert.True(_todosRegistros.Count() == 0);
            }
        }
    }
}
