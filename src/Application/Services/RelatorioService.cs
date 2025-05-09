using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;
        private readonly ISolicitanteRepository _solicitanteRepository;
        private readonly IDadoEpidemiologicoRepository _dadoEpidemiologicoRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;

        public RelatorioService(
            IRelatorioRepository relatorioRepository,
            ISolicitanteRepository solicitanteRepository,
            IDadoEpidemiologicoRepository dadoEpidemiologicoRepository,
            IHttpClientFactory httpClientFactory,
            IUnitOfWork unitOfWork)
        {
            _relatorioRepository = relatorioRepository;
            _solicitanteRepository = solicitanteRepository;
            _dadoEpidemiologicoRepository = dadoEpidemiologicoRepository;
            _httpClientFactory = httpClientFactory;
            _unitOfWork = unitOfWork;
        }

        public async Task<Relatorio> ProcessarRelatorioAsync(RelatorioRequestDto dto)
        {
            var solicitante = await _solicitanteRepository.GetByCpfAsync(dto.CPF).ConfigureAwait(false);

            if (solicitante == null)
            {
                solicitante = new Solicitante
                {
                    Nome = dto.Nome,
                    CPF = dto.CPF
                };

                await _solicitanteRepository.Add(solicitante).ConfigureAwait(false);

                // ✅ Salva o novo solicitante para garantir que o ID seja gerado
                if (_unitOfWork.Save() <= 0)
                    throw new Exception("Erro ao salvar o solicitante.");
            }

            var client = _httpClientFactory.CreateClient();
            string dadosUrl = MontarUrlDados(dto);

            var responseString = await client.GetStringAsync(dadosUrl).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(responseString))
                throw new Exception("A resposta da API de arboviroses está vazia.");

            var dadosEpidemiologicos = JsonSerializer.Deserialize<List<DadosEpidemiologicosDto>>(responseString);

            if (dadosEpidemiologicos == null || dadosEpidemiologicos.Count == 0)
                throw new Exception("Nenhum dado epidemiológico encontrado para o período informado.");

            string municipio = await ObterNomeMunicipioPorIBGE(dto.CodigoIBGE, client).ConfigureAwait(false);

            var relatorio = new Relatorio
            {
                DataSolicitacao = DateTime.Now,
                Arbovirose = dto.Arbovirose,
                SolicitanteId = solicitante.Id,
                SemanaInicio = dto.SemanaInicio,
                SemanaFim = dto.SemanaFim,
                CodigoIBGE = dto.CodigoIBGE,
                AnoFim = dto.AnoFim,
                AnoInicio = dto.AnoInicio,
                Municipio = municipio
            };

            await _relatorioRepository.Add(relatorio).ConfigureAwait(false);

            if (_unitOfWork.Save() <= 0)
                throw new Exception("Erro ao salvar o relatório.");

            int casos = 0;

            foreach (var dado in dadosEpidemiologicos)
            {
                casos += dado.casos;
                var dados = new DadoEpidemiologico
                {
                    RelatorioId = relatorio.Id,
                    DataInicioSemana = DateTimeOffset.FromUnixTimeMilliseconds(dado.data_iniSE).DateTime,
                    SemanaEpidemiologica = dado.SE,
                    Casos = dado.casos,
                    CasosEstimados = dado.casos_est,
                    Rt = dado.Rt,
                    NotificacoesAcumuladasAno = dado.notif_accum_year
                };

                await _dadoEpidemiologicoRepository.Add(dados).ConfigureAwait(false);
            }

            relatorio.TotalCasos = casos;
            _relatorioRepository.Update(relatorio);

            _unitOfWork.Save(); // ✅ Garante persistência dos dados epidemiológicos

            return relatorio;
        }

        private static string MontarUrlDados(RelatorioRequestDto dto)
        {
            return $"https://info.dengue.mat.br/api/alertcity?geocode={dto.CodigoIBGE}&disease={dto.Arbovirose}&format=json&ew_start={dto.SemanaInicio}&ew_end={dto.SemanaFim}&ey_start={dto.AnoInicio}&ey_end={dto.AnoFim}";
        }

        private static async Task<string> ObterNomeMunicipioPorIBGE(string codigoIbge, HttpClient client)
        {
            try
            {
                string ibgeUrl = $"https://servicodados.ibge.gov.br/api/v1/localidades/municipios/{codigoIbge}";
                var response = await client.GetAsync(ibgeUrl).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var ibgeData = JsonSerializer.Deserialize<IBGE>(json);
                    return ibgeData?.nome ?? "Desconhecido";
                }
            }
            catch
            {
                // Logue aqui se quiser
            }

            return "Erro ao obter município";
        }

        public async Task<IEnumerable<Relatorio>> ListarTodosAsync() =>
            await _relatorioRepository.GetAllWithRelationships().ConfigureAwait(false);

        public async Task<IEnumerable<Relatorio>> ListarPorIBGEAsync(string codigoIbge) =>
            await _relatorioRepository.GetByIbgeAsync(codigoIbge).ConfigureAwait(false);

        public async Task<IEnumerable<Relatorio>> ListarRelatoriosRjSpAsync() =>
            await _relatorioRepository.GetRelatoriosRjSpAsync().ConfigureAwait(false);

        public async Task<int> ListarTotalCasosRjSpAsync() =>
            await _relatorioRepository.GetTotalCasosRjSpAsync().ConfigureAwait(false);

        public async Task<int> ListarTotalCasosPorArboviroseAsync(string arbovirose) =>
            await _relatorioRepository.GetTotalCasosPorArboviroseAsync(arbovirose).ConfigureAwait(false);

        public async Task<IEnumerable<Solicitante>> ListarSolicitantesAsync() =>
            await _solicitanteRepository.GetAllWithRelationShips().ConfigureAwait(false);
    }
}
