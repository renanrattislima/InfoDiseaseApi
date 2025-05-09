namespace Infrastructure.Repositories
{
    using Domain.Interfaces;
    using Domain.Models;
    using Infrastructure.Extensions;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class SolicitanteRepository : GenericRepository<Solicitante>, ISolicitanteRepository
    {
        public SolicitanteRepository(DbContextClass dbContext) : base(dbContext)
        {
        }

        // Implementação específica para obter solicitante por CPF
        public async Task<Solicitante> GetByCpfAsync(string cpf)
        {
            return await _dbContext.Solicitantes
                .FirstOrDefaultAsync(s => s.CPF == cpf);
        }

        public async Task<IEnumerable<Solicitante>> GetAllWithRelationShips()
        {
            return await _dbContext.Solicitantes
                .IncludeRelatedEntities(_dbContext)
                .ToListAsync();
        }
    }
}
