using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Atendimento
{
    public class AtendimentoCompra
    {
        public int id_n_chamado { get; set; }
        public int id_status { get; set; }
        public int id_parceiro { get; set; }
        public int nota_fiscal { get; set; }
        public String tipo_atendimento { get; set; }
        public String cpf { get; set; }
        public DateTime data_compra { get; set; }
        public double valor_compra { get; set; }
        public DateTime data_abertura_chamado { get; set; }
        public DateTime data_fechamento_chamado { get; set; }
    }
}