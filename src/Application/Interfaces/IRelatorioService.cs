using Application.DTOs;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRelatorioService
    {
        Task<Relatorio> ProcessarRelatorioAsync(RelatorioRequestDto dto);
        Task<IEnumerable<Relatorio>> ListarTodosAsync();
        Task<IEnumerable<Relatorio>> ListarPorIBGEAsync(string codigoIbge);
        Task<IEnumerable<Relatorio>> ListarRelatoriosRjSpAsync();
        Task<int> ListarTotalCasosRjSpAsync();
        Task<int> ListarTotalCasosPorArboviroseAsync(string arbovirose);
        Task<IEnumerable<Solicitante>> ListarSolicitantesAsync();
    }
}
