using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Arbovirose { get; set; } = string.Empty;
        public int SemanaInicio { get; set; }
        public int SemanaFim { get; set; }
        public int AnoInicio { get; set; }
        public int AnoFim { get; set; }
        public string CodigoIBGE { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public int TotalCasos { get; set; }

        // Chave estrangeira para Solicitante
        public int SolicitanteId { get; set; }

        // Propriedade de navegação para Solicitante
        public Solicitante? Solicitante { get; set; }

        // Relacionamento com DadoEpidemiologico (1:N)
        public ICollection<DadoEpidemiologico> DadoEpidemiologicos { get; set; } = new List<DadoEpidemiologico>();
    }
}
