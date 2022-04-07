using Domain.Dtos.Cep;
using Domain.Dtos.Municipio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Integration.Test.Cep
{
    public class QuandoRequisitarCep : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Cep()
        {
            await AddToken();

            var municipioDto = new MunicipioDtoCreate()
            {
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6"),
                CodIBGE = 3550308,
                Nome = "São Paulo"
            };

            //POST
            var response = await PostJsonAsync(municipioDto, $"{hostApi}municipios", client);
            var postResult = await response.Content.ReadAsStringAsync();
            var registroPostMunicipio = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPostMunicipio.Nome);
            Assert.Equal(3550308, registroPostMunicipio.CodIBGE);
            Assert.True(registroPostMunicipio.Id != default(Guid));

            var cepDtoCreate = new CepDtoCreate
            {
                Cep = "12999000",
                Logradouro = "Rua teste",
                Numero = "S/N",
                MunicipioId = registroPostMunicipio.Id
            };

            response = await PostJsonAsync(cepDtoCreate, $"{hostApi}ceps", client);
            postResult = await response.Content.ReadAsStringAsync();
            var registroPost = JsonConvert.DeserializeObject<CepDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Rua teste", registroPost.Logradouro);
            Assert.Equal("S/N", registroPost.Numero);
            Assert.Equal("12999000", registroPost.Cep);
            Assert.True(registroPost.Id != default(Guid));

            //GETByID
            response = await client.GetAsync($"{hostApi}ceps/{registroPost.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var getResult = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(getResult);
            Assert.Equal(getResult.Id, registroPost.Id);
            Assert.Equal(getResult.Logradouro, registroPost.Logradouro);
            Assert.Equal(getResult.MunicipioId, registroPost.MunicipioId);
            Assert.Equal(getResult.Numero, registroPost.Numero);

            //UPDATE
            var cepDtoUpdate = new CepDtoUpdate
            {
                Cep = "12999000",
                Logradouro = "Rua teste 2",
                Numero = "299",
                MunicipioId = getResult.MunicipioId,
                Id = getResult.Id
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(cepDtoUpdate),
                Encoding.UTF8, "application/json");

            response = await client.PutAsync($"{hostApi}ceps", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroUpdate = JsonConvert.DeserializeObject<CepDtoUpdateResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroUpdate.Logradouro, registroPost.Logradouro);
            Assert.NotEqual(registroUpdate.Numero, registroPost.Numero);
            Assert.Equal(registroUpdate.Logradouro, cepDtoUpdate.Logradouro);
            Assert.Equal(registroUpdate.Id, cepDtoUpdate.Id);
            Assert.Equal(registroUpdate.Numero, cepDtoUpdate.Numero);
            Assert.Equal(registroUpdate.Cep, cepDtoUpdate.Cep);
            Assert.Equal(registroUpdate.MunicipioId, cepDtoUpdate.MunicipioId);

            //GETByCep
            response = await client.GetAsync($"{hostApi}ceps/byCep/{registroUpdate.Cep}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var getResultCep = JsonConvert.DeserializeObject<CepDto>(jsonResult);
            Assert.NotNull(getResultCep);
            Assert.NotNull(getResultCep.Municipio);
            Assert.NotNull(getResultCep.Municipio.Uf);
            Assert.Equal(getResultCep.Logradouro, registroUpdate.Logradouro);
            Assert.Equal(getResultCep.Numero, registroUpdate.Numero);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}ceps/{getResultCep.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //GETByID
            response = await client.GetAsync($"{hostApi}ceps/{getResultCep.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
