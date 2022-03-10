using Api.Domain.Entities;
using AutoMapper;
using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Domain.Dtos.Uf;
using Domain.Dtos.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Mappings
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<UserDto, UserEntity>()
                .ReverseMap();
            CreateMap<UserDtoCreate, UserEntity>()
                .ReverseMap();
            CreateMap<UserDtoUpdate, UserEntity>()
                .ReverseMap();
            CreateMap<UserDtoCreateResult, UserEntity>()
                .ReverseMap();
            CreateMap<UserDtoUpdateResult, UserEntity>()
                .ReverseMap();

            CreateMap<UfDto, UfEntity>()
                .ReverseMap();

            CreateMap<MunicipioDto, MunicipioEntity>()
                .ReverseMap();
            CreateMap<MunicipioDtoCompleto, MunicipioEntity>()
                .ReverseMap();
            CreateMap<MunicipioDtoCreateResult, MunicipioEntity>()
                .ReverseMap();
            CreateMap<MunicipioDtoUpdateResult, MunicipioEntity>()
                .ReverseMap();

            CreateMap<CepDto, CepEntity>()
                .ReverseMap();
            CreateMap<CepDtoCreateResult, CepEntity>()
                .ReverseMap();
            CreateMap<CepDtoUpdateResult, CepEntity>()
                .ReverseMap();
        }
    }
}
