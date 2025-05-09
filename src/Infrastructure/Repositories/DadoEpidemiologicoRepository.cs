namespace Infrastructure.Repositories
{
    using Domain.Interfaces;
    using Domain.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class DadoEpidemiologicoRepository : GenericRepository<DadoEpidemiologico>, IDadoEpidemiologicoRepository
    {
        public DadoEpidemiologicoRepository(DbContextClass dbContext) : base(dbContext)
        {
        }
    }
}
