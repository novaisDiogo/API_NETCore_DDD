using Domain.Dtos.Municipio;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class MunicipioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os modelos de Municipio")]
        public void E_Possivel_Mapear_os_Modelos_Municipio()
        {
            var model = new MunicipioModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.City(),
                CodIBGE = Faker.RandomNumber.Next(1, 10000),
                UfId = Guid.NewGuid(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listEntity = new List<MunicipioEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new MunicipioEntity()
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
                };

                listEntity.Add(item);
            }

            //model => entity
            var entity = Mapper.Map<MunicipioEntity>(model);
            Assert.Equal(entity.Id, model.Id);
            Assert.Equal(entity.Nome, model.Nome);
            Assert.Equal(entity.CodIBGE, model.CodIBGE);
            Assert.Equal(entity.UfId, model.UfId);
            Assert.Equal(entity.CreateAt, model.CreateAt);
            Assert.Equal(entity.UpdateAt, model.UpdateAt);

            //entity => dto
            var municipioDto = Mapper.Map<MunicipioDto>(entity);
            Assert.Equal(entity.Id, municipioDto.Id);
            Assert.Equal(entity.Nome, municipioDto.Nome);
            Assert.Equal(entity.CodIBGE, municipioDto.CodIBGE);
            Assert.Equal(entity.UfId, municipioDto.UfId);

            var municipioDtoCompleto = Mapper.Map<MunicipioDtoCompleto>(listEntity.FirstOrDefault());
            Assert.Equal(municipioDtoCompleto.Id, listEntity.FirstOrDefault().Id);
            Assert.Equal(municipioDtoCompleto.Nome, listEntity.FirstOrDefault().Nome);
            Assert.Equal(municipioDtoCompleto.CodIBGE, listEntity.FirstOrDefault().CodIBGE);
            Assert.Equal(municipioDtoCompleto.UfId, listEntity.FirstOrDefault().UfId);
            Assert.NotNull(municipioDtoCompleto.Uf);

            var listDto = Mapper.Map<List<MunicipioDto>>(listEntity);
            Assert.True(listDto.Count() == listEntity.Count());
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(listDto[i].Id, listEntity[i].Id);
                Assert.Equal(listDto[i].Nome, listEntity[i].Nome);
                Assert.Equal(listDto[i].CodIBGE, listEntity[i].CodIBGE);
                Assert.Equal(listDto[i].UfId, listEntity[i].UfId);
            }

            var municipioDtoCreateResult = Mapper.Map<MunicipioDtoCreateResult>(entity);
            Assert.Equal(entity.Id, municipioDtoCreateResult.Id);
            Assert.Equal(entity.Nome, municipioDtoCreateResult.Nome);
            Assert.Equal(entity.CodIBGE, municipioDtoCreateResult.CodIBGE);
            Assert.Equal(entity.UfId, municipioDtoCreateResult.UfId);
            Assert.Equal(entity.CreateAt, municipioDtoCreateResult.CreateAt);

            var municipioDtoUpdateResult = Mapper.Map<MunicipioDtoUpdateResult>(entity);
            Assert.Equal(entity.Id, municipioDtoUpdateResult.Id);
            Assert.Equal(entity.Nome, municipioDtoUpdateResult.Nome);
            Assert.Equal(entity.CodIBGE, municipioDtoUpdateResult.CodIBGE);
            Assert.Equal(entity.UfId, municipioDtoUpdateResult.UfId);
            Assert.Equal(entity.UpdateAt, municipioDtoUpdateResult.UpdateAt);

            //dto => model
            var ufModel = Mapper.Map<MunicipioModel>(municipioDto);
            Assert.Equal(ufModel.Id, municipioDto.Id);
            Assert.Equal(ufModel.Nome, municipioDto.Nome);
            Assert.Equal(ufModel.CodIBGE, municipioDto.CodIBGE);
            Assert.Equal(ufModel.UfId, municipioDto.UfId);

            var ufDtoCreate = Mapper.Map<MunicipioDtoCreate>(ufModel);
            Assert.Equal(ufModel.Nome, ufDtoCreate.Nome);
            Assert.Equal(ufModel.CodIBGE, ufDtoCreate.CodIBGE);
            Assert.Equal(ufModel.UfId, ufDtoCreate.UfId);

            var ufDtoUpdate= Mapper.Map<MunicipioDtoUpdate>(ufModel);
            Assert.Equal(ufModel.Id, ufDtoUpdate.Id);
            Assert.Equal(ufModel.Nome, ufDtoUpdate.Nome);
            Assert.Equal(ufModel.CodIBGE, ufDtoUpdate.CodIBGE);
            Assert.Equal(ufModel.UfId, ufDtoUpdate.UfId);


        }
    }
}
