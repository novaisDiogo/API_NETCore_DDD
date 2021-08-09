using Domain.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Test.Users
{
    public class UserTests
    {
        public static string NomeUsuario { get; set; }
        public static string EmailUsuario { get; set; }
        public static string NomeUsuarioAlterado { get; set; }
        public static string EmailUsuarioAlterado { get; set; }
        public static Guid IdUsuario { get; set; }
        public List<UserDto> listUserDto { get; set; } = new List<UserDto>();
        public UserDto userDto;
        public UserDtoCreate userDtoCreate;
        public UserDtoCreateResult dtoCreateResult;
        public UserDtoUpdate userDtoUpdate;
        public UserDtoUpdateResult dtoUpdateResult;

        public UserTests()
        {
            IdUsuario = Guid.NewGuid();
            NomeUsuario = Faker.Name.FullName();
            EmailUsuario = Faker.Internet.Email();
            NomeUsuarioAlterado = Faker.Name.FullName();
            EmailUsuarioAlterado = Faker.Internet.Email();

            for (int i = 0; i < 10; i++)
            {
                var dto = new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                listUserDto.Add(dto);
            }

            userDto = new UserDto
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            userDtoCreate = new UserDtoCreate
            {
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            dtoCreateResult = new UserDtoCreateResult
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario,
                CreateAt = DateTime.UtcNow
            };

            userDtoUpdate = new UserDtoUpdate
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario
            };

            dtoUpdateResult = new UserDtoUpdateResult
            {
                Id = IdUsuario,
                Name = NomeUsuario,
                Email = EmailUsuario,
                UpdateAt = DateTime.UtcNow
            };
        }
    }
}
