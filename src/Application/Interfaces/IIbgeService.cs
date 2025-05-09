using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIbgeService
    {
        Task<List<IBGE>> ObterMunicipiosAsync(string uf);
    }
}
