using Domain.Dtos.Cep;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class CepMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os modelos de Cep")]
        public void E_Possivel_Mapear_os_Modelos_Cep()
        {
            var model = new CepModel()
            {
                Id = Guid.NewGuid(),
                Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                Logradouro = Faker.Address.StreetName(),
                Numero = "",
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                MunicipioId = Guid.NewGuid()
            };

            var listEntity = new List<CepEntity>();
            for(int i = 0;i < 5; i++)
            {
                var item = new CepEntity()
                {
                    Id = Guid.NewGuid(),
                    Cep = Faker.RandomNumber.Next(1, 10000).ToString(),
                    Logradouro = Faker.Address.StreetName(),
                    Numero = "",
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    MunicipioId = Guid.NewGuid(),
                    Municipio = new MunicipioEntity
                    {
                        Id = Guid.NewGuid(),
                        Nome = Faker.Address.City(),
                        CodIBGE = Faker.RandomNumber.Next(1, 10000),
                        UfId = Guid.NewGuid(),
                        CreateAt = DateTime.UtcNow,
                        UpdateAt = DateTime.UtcNow,
                        Uf = new UfEntity
                        {
                            Id = Guid.NewGuid(),
                            Nome = Faker.Address.UsState(),
                            Sigla = Faker.Address.UsState().Substring(1, 3)
                        }
                    }
                };

                listEntity.Add(item);
            }

            //model => entity
            var entity = Mapper.Map<CepEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Logradouro, model.Logradouro);
            Assert.Equal(entity.Numero, model.Numero);
            Assert.Equal(entity.Cep, model.Cep);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //entity => dto
            var cepDto = Mapper.Map<CepDto>(entity);
            Assert.Equal(entity.Id, cepDto.Id);
            Assert.Equal(entity.Logradouro, cepDto.Logradouro);
            Assert.Equal(entity.Numero, cepDto.Numero);
            Assert.Equal(entity.Cep, cepDto.Cep);

            var cepDtoCompleto = Mapper.Map<CepDto>(listEntity.FirstOrDefault());
            Assert.Equal(cepDtoCompleto.Id, listEntity.FirstOrDefault().Id);
            Assert.Equal(cepDtoCompleto.Logradouro, listEntity.FirstOrDefault().Logradouro);
            Assert.Equal(cepDtoCompleto.Numero, listEntity.FirstOrDefault().Numero);
            Assert.Equal(cepDtoCompleto.Cep, listEntity.FirstOrDefault().Cep);
            Assert.NotNull(cepDtoCompleto.Municipio);
            Assert.NotNull(cepDtoCompleto.Municipio.Uf);

            var listDto = Mapper.Map<List<CepDto>>(listEntity);
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(listDto[i].Id, listEntity[i].Id);
                Assert.Equal(listDto[i].Logradouro, listEntity[i].Logradouro);
                Assert.Equal(listDto[i].Numero, listEntity[i].Numero);
                Assert.Equal(listDto[i].Cep, listEntity[i].Cep);
            }

            var cepDtoCreateResult = Mapper.Map<CepDtoCreateResult>(entity);
            Assert.Equal(entity.Id, cepDtoCreateResult.Id);
            Assert.Equal(entity.Logradouro, cepDtoCreateResult.Logradouro);
            Assert.Equal(entity.Numero, cepDtoCreateResult.Numero);
            Assert.Equal(entity.Cep, cepDtoCreateResult.Cep);
            Assert.Equal(entity.CreateAt, cepDtoCreateResult.CreateAt);

            var cepDtoUpdateResult = Mapper.Map<CepDtoUpdateResult>(entity);
            Assert.Equal(entity.Id, cepDtoUpdateResult.Id);
            Assert.Equal(entity.Logradouro, cepDtoUpdateResult.Logradouro);
            Assert.Equal(entity.Numero, cepDtoUpdateResult.Numero);
            Assert.Equal(entity.Cep, cepDtoUpdateResult.Cep);
            Assert.Equal(entity.UpdateAt, cepDtoUpdateResult.UpdateAt);

            //dto => model
            cepDto.Numero = "";
            var cepModel = Mapper.Map<CepModel>(cepDto);
            Assert.Equal(cepModel.Id, cepDto.Id);
            Assert.Equal(cepModel.Logradouro, cepDto.Logradouro);
            Assert.Equal(cepModel.Cep, cepDto.Cep);
            Assert.Equal(cepModel.Numero, "S/N");

            var cepDtoCreate = Mapper.Map<CepDtoCreate>(cepModel);
            Assert.Equal(cepModel.Numero, cepDtoCreate.Numero);
            Assert.Equal(cepModel.Logradouro, cepDtoCreate.Logradouro);
            Assert.Equal(cepModel.Cep, cepDtoCreate.Cep);
        }
    }
}
