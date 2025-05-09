using Application.DTOs;
using Application.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class IbgeService : IIbgeService
    {
        private readonly HttpClient _httpClient;

        public IbgeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IBGE>> ObterMunicipiosAsync(string uf)
        {

            var url = $"https://servicodados.ibge.gov.br/api/v1/localidades/estados/{uf}/municipios";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Erro ao consultar a API do IBGE: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<IBGE>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
