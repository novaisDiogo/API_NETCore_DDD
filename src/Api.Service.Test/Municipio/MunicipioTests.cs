using Domain.Dtos.Municipio;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test.Municipio
{
    public class MunicipioTests : BaseTestService
    {
        public static string NomeMunicipio { get; set; }
        public static int CodigoIBGEMunicipio { get; set; }
        public static string NomeMunicipioAlterado { get; set; }
        public static int CodigoIBGEMunicipioAlterado { get; set; }
        public static Guid IdMunicipio { get; set; }
        public static Guid IdUf { get; set; }
        public static DateTime CreateAt { get; set; }
        public static DateTime UpdateAt { get; set; }
        public static string NomeUf { get; set; }
        public static string SiglaUf { get; set; }

        public MunicipioEntity municipioEntity;
        public List<MunicipioEntity> municipioList = new List<MunicipioEntity>();
        public MunicipioDtoCreate municipioDtoCreate;
        public MunicipioDtoUpdate municipioDtoUpdate;

        public MunicipioTests()
        {
            IdMunicipio = Guid.NewGuid();
            NomeMunicipio = Faker.Address.StreetName();
            CodigoIBGEMunicipio = Faker.RandomNumber.Next(1, 10000);
            NomeMunicipioAlterado = Faker.Address.StreetName();
            CodigoIBGEMunicipioAlterado = Faker.RandomNumber.Next(1, 10000);
            IdUf = Guid.NewGuid();
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            NomeUf = Faker.Address.UsState();
            SiglaUf = Faker.Address.UsState().Substring(1, 3);

            for (int i = 0; i < 10; i++)
            {
                var municipioUf = new MunicipioEntity 
                { 
                    Id = Guid.NewGuid(),
                    Nome = Faker.Name.FullName(),
                    CodIBGE = Faker.RandomNumber.Next(1, 10000),
                    UfId = Guid.NewGuid(),
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow
                };
                municipioList.Add(municipioUf);
            }

            municipioEntity = new MunicipioEntity
            {
                Id = IdMunicipio,
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf,
                CreateAt = CreateAt,
                UpdateAt = UpdateAt,
                Uf = new UfEntity
                {
                    Id = IdUf,
                    Nome = NomeUf,
                    Sigla = SiglaUf
                }
            };

            municipioDtoCreate = new MunicipioDtoCreate
            {
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf
            };

            municipioDtoUpdate = new MunicipioDtoUpdate
            {
                Nome = NomeMunicipio,
                CodIBGE = CodigoIBGEMunicipio,
                UfId = IdUf,
                Id = IdMunicipio
            };
        }
    }
}
