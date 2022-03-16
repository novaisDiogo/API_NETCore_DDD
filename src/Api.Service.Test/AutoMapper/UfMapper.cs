using Domain.Dtos.Uf;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UfMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel Mapear os modelos de UF")]
        public void E_Possivel_Mapear_os_Modelos_UF()
        {
            var model = new UfModel
            {
                Id = Guid.NewGuid(),
                Nome = Faker.Address.UsState(),
                Sigla = Faker.Address.UsState().Substring(1, 3),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listaEntity = new List<UfEntity>();
            for(int i = 0; i < 5; i++)
            {
                var item = new UfEntity
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                listaEntity.Add(item);
            }

            //model => entity
            var entity = Mapper.Map<UfEntity>(model);

            Assert.Equal(model.Id, entity.Id);
            Assert.Equal(model.Nome, entity.Nome);
            Assert.Equal(model.Sigla, entity.Sigla);
            Assert.Equal(model.CreateAt, entity.CreateAt);
            Assert.Equal(model.UpdateAt, entity.UpdateAt);

            //entity => dto
            var ufDto = Mapper.Map<UfDto>(entity);
            Assert.Equal(ufDto.Id, entity.Id);
            Assert.Equal(ufDto.Nome, entity.Nome);
            Assert.Equal(ufDto.Sigla, entity.Sigla);

            var listDto = Mapper.Map<List<UfDto>>(listaEntity);
            Assert.True(listDto.Count() == listaEntity.Count());
            for (int i = 0; i < 5; i++)
            {
                Assert.Equal(listDto[i].Id, listaEntity[i].Id);
                Assert.Equal(listDto[i].Nome, listaEntity[i].Nome);
                Assert.Equal(listDto[i].Sigla, listaEntity[i].Sigla);
            }

            //model => dto
            ufDto = Mapper.Map<UfDto>(model);
            Assert.Equal(ufDto.Id, model.Id);
            Assert.Equal(ufDto.Nome, model.Nome);
            Assert.Equal(ufDto.Sigla, model.Sigla);
        }
    }
}
