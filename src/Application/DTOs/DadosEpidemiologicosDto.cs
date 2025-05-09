using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DadosEpidemiologicosDto
    {
        public long data_iniSE { get; set; }
        public int SE { get; set; }
        public double? casos_est { get; set; }
        public double? casos_est_min { get; set; }
        public double? casos_est_max { get; set; }
        public int casos { get; set; }
        public double? p_rt1 { get; set; }
        public double? p_inc100k { get; set; }
        public int Localidade_id { get; set; }
        public int nivel { get; set; }
        public long id { get; set; }
        public string versao_modelo { get; set; }
        public double? tweet { get; set; }
        public double? Rt { get; set; }
        public double? pop { get; set; }
        public double? tempmin { get; set; }
        public double? umidmax { get; set; }
        public int receptivo { get; set; }
        public int transmissao { get; set; }
        public int nivel_inc { get; set; }
        public double? umidmed { get; set; }
        public double? umidmin { get; set; }
        public double? tempmed { get; set; }
        public double? tempmax { get; set; }
        public int casprov { get; set; }
        public double? casprov_est { get; set; }
        public double? casprov_est_min { get; set; }
        public double? casprov_est_max { get; set; }
        public int? casconf { get; set; }
        public int notif_accum_year { get; set; }
    }

}
