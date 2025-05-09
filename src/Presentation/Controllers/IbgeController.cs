using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IbgeController : ControllerBase
    {
        private readonly IIbgeService _ibgeService;

        public IbgeController(IIbgeService ibgeService)
        {
            _ibgeService = ibgeService;
        }

        [HttpGet("municipios")]
        public async Task<IActionResult> ObterMunicipios(string uf)
        {
            var municipios = await _ibgeService.ObterMunicipiosAsync(uf);
            return Ok(municipios);
        }
    }
}
