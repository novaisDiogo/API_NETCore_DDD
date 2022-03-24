using Domain.Dtos.Cep;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test.Cep
{
    public class CepTests : BaseTestService
    {
        public Guid IdCep { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public Guid MunicipioId { get; set; }
        public Guid UfId;
        public CepEntity cepEntity;
        public CepDtoCreate cepDtoCreate;
        public CepDtoUpdate CepDtoUpdate;

        public CepTests()
        {
            IdCep = Guid.NewGuid();
            Cep = "13.481-001";
            Logradouro = Faker.Address.StreetName();
            Numero = "0 até 2000";
            MunicipioId = Guid.NewGuid();
            UfId = Guid.NewGuid();

            cepEntity = new CepEntity
            {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                Numero = Numero,
                MunicipioId = MunicipioId,
                Municipio = new MunicipioEntity
                {
                    Id = MunicipioId,
                    Nome = Faker.Name.FullName(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = UfId,
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                    Uf = new UfEntity
                    {
                        Id = UfId,
                        Nome = Faker.Address.UsState(),
                        Sigla = Faker.Address.UsState().Substring(1, 3)
                    }
                },
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow
            };

            cepDtoCreate = new CepDtoCreate
            {
                Cep = Cep,
                Logradouro = Logradouro,
                MunicipioId = MunicipioId,
                Numero = Numero
            };

            CepDtoUpdate = new CepDtoUpdate
            {
                Id = IdCep,
                Cep = Cep,
                Logradouro = Logradouro,
                MunicipioId = MunicipioId,
                Numero = Numero
            };
        }
    }
}
