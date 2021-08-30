using Api.Domain.Entities;
using Domain.Dtos.User;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Api.Service.Test.AutoMapper
{
    public class UsuarioMapper : BaseTestService
    {
        [Fact(DisplayName = "É possivel mapear os modelos")]
        public void E_Possivel_Mapear_os_Modelos()
        {
            var model = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = Faker.Name.FullName(),
                Email = Faker.Internet.Email(),
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            var listEntity = new List<UserEntity>();
            for (int i = 0; i < 5; i++)
            {
                var item = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };

                listEntity.Add(item);
            }

            //Model => Entity
            var modelToEntity = Mapper.Map<UserEntity>(model);
            Assert.Equal(modelToEntity.Id, model.Id);
            Assert.Equal(modelToEntity.Name, model.Name);
            Assert.Equal(modelToEntity.Email, model.Email);
            Assert.Equal(modelToEntity.CreateAt, model.CreateAt);
            Assert.Equal(modelToEntity.UpdateAt, model.UpdateAt);

            //Entity => Dto
            var userDto = Mapper.Map<UserDto>(modelToEntity);
            Assert.Equal(userDto.Id, modelToEntity.Id);
            Assert.Equal(userDto.Name, modelToEntity.Name);
            Assert.Equal(userDto.Email, modelToEntity.Email);

            var listaDto = Mapper.Map<List<UserDto>>(listEntity);
            Assert.True(listaDto.Count() == listEntity.Count());
            for (int i = 0; i < listaDto.Count(); i++)
            {
                Assert.Equal(listaDto[i].Id, listEntity[i].Id);
                Assert.Equal(listaDto[i].Name, listEntity[i].Name);
                Assert.Equal(listaDto[i].Email, listEntity[i].Email);
            }

            var userDtoCreateResult = Mapper.Map<UserDtoCreateResult>(modelToEntity);
            Assert.Equal(modelToEntity.Id, userDtoCreateResult.Id);
            Assert.Equal(modelToEntity.Name, userDtoCreateResult.Name);
            Assert.Equal(modelToEntity.Email, userDtoCreateResult.Email);
            Assert.Equal(modelToEntity.CreateAt, userDtoCreateResult.CreateAt);

            var userDtoUpdateResult = Mapper.Map<UserDtoUpdateResult>(modelToEntity);
            Assert.Equal(modelToEntity.Id, userDtoUpdateResult.Id);
            Assert.Equal(modelToEntity.Name, userDtoUpdateResult.Name);
            Assert.Equal(modelToEntity.Email, userDtoUpdateResult.Email);
            Assert.Equal(modelToEntity.UpdateAt, userDtoUpdateResult.UpdateAt);

            //Dto => model
            var modelDto = Mapper.Map<UserModel>(userDto);
            Assert.Equal(modelDto.Id, userDto.Id);
            Assert.Equal(modelDto.Name, userDto.Name);
            Assert.Equal(modelDto.Email, userDto.Email);

            var userDtoCreate = Mapper.Map<UserDtoCreate>(modelDto);
            Assert.Equal(modelDto.Name, userDtoCreate.Name);
            Assert.Equal(modelDto.Email, userDtoCreate.Email);

            var userDtoUpdate = Mapper.Map<UserDtoUpdate>(modelDto);
            Assert.Equal(modelDto.Id, userDtoUpdate.Id);
            Assert.Equal(modelDto.Name, userDtoUpdate.Name);
            Assert.Equal(modelDto.Email, userDtoUpdate.Email);
        }
    }
}
