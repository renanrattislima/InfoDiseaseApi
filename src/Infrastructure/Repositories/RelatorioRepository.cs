namespace Infrastructure.Repositories
{
    using Domain.Interfaces;
    using Domain.Models;
    using Infrastructure.Extensions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RelatorioRepository : GenericRepository<Relatorio>, IRelatorioRepository
    {
        public RelatorioRepository(DbContextClass dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Relatorio>> GetAllWithRelationships()
        {
            return await _dbContext.Relatorios
                .IncludeRelatedEntities(_dbContext)  
                .ToListAsync();
        }

        // Implementação específica para obter relatórios por código IBGE
        public async Task<IEnumerable<Relatorio>> GetByIbgeAsync(string codigoIbge)
        {
            return await _dbContext.Relatorios
                .IncludeRelatedEntities(_dbContext)  // Incluindo as entidades relacionadas
                .Where(r => r.CodigoIBGE == codigoIbge)
                .ToListAsync();
        }

        // Implementação para obter relatórios específicos de RJ e SP
        public async Task<IEnumerable<Relatorio>> GetRelatoriosRjSpAsync()
        {
            return await _dbContext.Relatorios
                .IncludeRelatedEntities(_dbContext)  // Incluindo as entidades relacionadas
                .Where(r => r.Municipio == "Rio de Janeiro" || r.Municipio == "São Paulo")
                .ToListAsync();
        }

        // Implementação para obter total de casos no RJ e SP
        public async Task<int> GetTotalCasosRjSpAsync()
        {
            return await _dbContext.Relatorios
                .Where(r => r.Municipio == "Rio de Janeiro" || r.Municipio == "São Paulo")
                .SumAsync(r => r.TotalCasos);
        }

        // Implementação para obter total de casos por tipo de arbovirose
        public async Task<int> GetTotalCasosPorArboviroseAsync(string arbovirose)
        {
            return await _dbContext.Relatorios
                .Where(r => r.Arbovirose == arbovirose)
                .SumAsync(r => r.TotalCasos);
        }
    }
}
