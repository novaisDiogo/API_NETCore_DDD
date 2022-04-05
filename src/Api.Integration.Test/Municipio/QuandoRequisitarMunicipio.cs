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

namespace Api.Integration.Test.Municipio
{
    public class QuandoRequisitarMunicipio : BaseIntegration
    {
        [Fact]
        public async Task E_Possivel_Realizar_Crud_Municipio()
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
            var registroPost = JsonConvert.DeserializeObject<MunicipioDtoCreateResult>(postResult);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("São Paulo", registroPost.Nome);
            Assert.Equal(3550308, registroPost.CodIBGE);
            Assert.True(registroPost.Id != default(Guid));

            //GetAll
            response = await client.GetAsync($"{hostApi}municipios");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var jsonResult = await response.Content.ReadAsStringAsync();
            var listFromResult = JsonConvert.DeserializeObject<IEnumerable<MunicipioDto>>(jsonResult);
            Assert.NotNull(listFromResult);
            Assert.True(listFromResult.Count() > 0);
            Assert.True(listFromResult.Where(r => r.Id == registroPost.Id).Any());

            //UPDATE
            var updateMunicipioDto = new MunicipioDtoUpdate 
            { 
                Id = registroPost.Id,
                Nome = "Limeira",
                CodIBGE = 3526902,
                UfId = new Guid("e7e416de-477c-4fa3-a541-b5af5f35ccf6")
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(updateMunicipioDto), 
                Encoding.UTF8,"application/json");

            response = await client.PutAsync($"{hostApi}municipios", stringContent);
            jsonResult = await response.Content.ReadAsStringAsync();
            var registroUpdate = JsonConvert.DeserializeObject<MunicipioDtoUpdateResult>(jsonResult);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(registroUpdate.Nome, registroPost.Nome);
            Assert.NotEqual(registroUpdate.CodIBGE, registroPost.CodIBGE);

            //GETComplete/ID
            response = await client.GetAsync($"{hostApi}Municipios/Complete/{registroUpdate.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var getCompleteIdResult = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(getCompleteIdResult);
            Assert.Equal(getCompleteIdResult.Nome, registroUpdate.Nome);
            Assert.Equal(getCompleteIdResult.CodIBGE, registroUpdate.CodIBGE);
            Assert.NotNull(getCompleteIdResult.Uf);
            Assert.Equal("São Paulo", getCompleteIdResult.Uf.Nome);
            Assert.Equal("SP", getCompleteIdResult.Uf.Sigla); 

            //GETComplete/IBGE
            response = await client.GetAsync($"{hostApi}Municipios/byIBGE/{registroUpdate.CodIBGE}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            jsonResult = await response.Content.ReadAsStringAsync();
            var getCompleteIBGEResult = JsonConvert.DeserializeObject<MunicipioDtoCompleto>(jsonResult);
            Assert.NotNull(getCompleteIBGEResult);
            Assert.Equal(getCompleteIBGEResult.Nome, registroUpdate.Nome);
            Assert.Equal(getCompleteIBGEResult.CodIBGE, registroUpdate.CodIBGE);
            Assert.NotNull(getCompleteIBGEResult.Uf);
            Assert.Equal("São Paulo", getCompleteIdResult.Uf.Nome);
            Assert.Equal("SP", getCompleteIdResult.Uf.Sigla);

            //DELETE
            response = await client.DeleteAsync($"{hostApi}municipios/{getCompleteIBGEResult.Id}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            //GET ID After DELETE
            response = await client.GetAsync($"{hostApi}municipios/{getCompleteIBGEResult.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
