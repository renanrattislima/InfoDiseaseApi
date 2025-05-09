using AutoMapper;
using Presentation.Models.Request;

namespace Presentation.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeamento do DTO de Presentation para Application
            CreateMap<RelatorioRequestDto, Application.DTOs.RelatorioRequestDto>();
            CreateMap<Application.DTOs.RelatorioRequestDto, RelatorioRequestDto>();
        }
    }
}
