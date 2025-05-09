using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.Request;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _service;
        private readonly IMapper _mapper;

        public RelatoriosController(IRelatorioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("consultar")]
        public async Task<IActionResult> Consultar([FromBody] RelatorioRequestDto dto)
        {
            var applicationDto = _mapper.Map<Application.DTOs.RelatorioRequestDto>(dto);
            var relatorio = await _service.ProcessarRelatorioAsync(applicationDto);
            return Ok(relatorio);
        }

        // GET /api/relatorios
        [HttpGet]
        public async Task<IActionResult> GetTodosRelatorios()
        {
            var relatorios = await _service.ListarTodosAsync();
            return Ok(relatorios);
        }

        // GET /api/relatorios/municipios?codigoIbge={codigoIbge}
        [HttpGet("municipios")]
        public async Task<IActionResult> GetRelatoriosPorMunicipio(string codigoIbge)
        {
            var relatorios = await _service.ListarPorIBGEAsync(codigoIbge);
            return Ok(relatorios);
        }

        // GET /api/relatorios/rj-sp
        [HttpGet("rj-sp")]
        public async Task<IActionResult> GetRelatoriosRjSp()
        {
            var relatorios = await _service.ListarRelatoriosRjSpAsync();
            return Ok(relatorios);
        }

        // GET /api/relatorios/total/rj-sp
        [HttpGet("total/rj-sp")]
        public async Task<IActionResult> GetTotalCasosRjSp()
        {
            var totalCasos = await _service.ListarTotalCasosRjSpAsync();
            return Ok(totalCasos);
        }

        // GET /api/relatorios/total/arbovirose
        [HttpGet("total/arbovirose")]
        public async Task<IActionResult> GetTotalCasosPorArbovirose([FromQuery] string doenca)
        {
            if (string.IsNullOrWhiteSpace(doenca))
                return BadRequest("Parâmetro 'doenca' é obrigatório.");

            var totalArbovirose = await _service.ListarTotalCasosPorArboviroseAsync(doenca.ToLower());
            return Ok(totalArbovirose);
        }

        // GET /api/solicitantes
        [HttpGet("solicitantes")]
        public async Task<IActionResult> GetSolicitantes()
        {
            var solicitantes = await _service.ListarSolicitantesAsync();
            return Ok(solicitantes);
        }
    }
}
