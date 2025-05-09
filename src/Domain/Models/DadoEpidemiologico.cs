using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DadoEpidemiologico
    {
        public int Id { get; set; }
        public DateTime DataInicioSemana { get; set; }
        public int SemanaEpidemiologica { get; set; }
        public double Casos { get; set; }
        public double? CasosEstimados { get; set; }
        public double? Rt { get; set; }
        public int NotificacoesAcumuladasAno { get; set; }

        public int RelatorioId { get; set; }
        public Relatorio Relatorio { get; set; }
    }
}
