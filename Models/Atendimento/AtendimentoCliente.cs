using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Atendimento
{
    public class AtendimentoCliente
    {
        public int idUsuarioAtend { get; set; }
        public int id_n_chamado { get; set; }
        public String nomeUser { get; set; }
        public int id_status { get; set; }
        public int id_usuario { get; set; }
        public String cpf { get; set; }
        public String tipo_atendimento { get; set; }
        public String descricao_chamado { get; set; }
        public DateTime data_abertura_chamado { get; set; }
        public DateTime data_fechamento_chamado { get; set; }
        public AtendimentoCliente categoria { get; set; }
    }
}