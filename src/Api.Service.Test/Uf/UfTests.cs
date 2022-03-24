using Domain.Dtos.Uf;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test.Uf
{
    public class UfTests : BaseTestService
    {
        public static string Nome { get; set; }
        public static string Sigla { get; set; }
        public static Guid IdUf { get; set; }

        public List<UfEntity> UfDtoList = new List<UfEntity>();
        public UfEntity ufEntity;

        public UfTests()
        {
            IdUf = Guid.NewGuid();
            Sigla = Faker.Address.UsState().Substring(1, 3);
            Nome = Faker.Address.UsState();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UfEntity()
                {
                    Id = Guid.NewGuid(),
                    Nome = Faker.Address.UsState(),
                    Sigla = Faker.Address.UsState().Substring(1, 3)
                };

                UfDtoList.Add(dto);
            }

            ufEntity = new UfEntity
            {
                Id = IdUf,
                Nome = Nome,
                Sigla = Sigla
            };
        }
    }
}
