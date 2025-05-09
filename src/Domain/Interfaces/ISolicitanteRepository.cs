namespace Domain.Interfaces
{
    using Domain.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISolicitanteRepository : IGenericRepository<Solicitante>
    {
        Task<Solicitante> GetByCpfAsync(string cpf);
        Task<IEnumerable<Solicitante>> GetAllWithRelationShips();
    }
}
