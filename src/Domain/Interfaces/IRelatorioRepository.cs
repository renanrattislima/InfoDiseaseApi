namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRelatorioRepository : IGenericRepository<Relatorio>
    {
        Task<IEnumerable<Relatorio>> GetAllWithRelationships();
        Task<IEnumerable<Relatorio>> GetByIbgeAsync(string codigoIbge);
        Task<IEnumerable<Relatorio>> GetRelatoriosRjSpAsync();
        Task<int> GetTotalCasosRjSpAsync();
        Task<int> GetTotalCasosPorArboviroseAsync(string arbovirose);
    }
}
