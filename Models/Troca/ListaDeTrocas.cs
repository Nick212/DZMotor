using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Troca
{
    public class ListaDeTrocas
    {
        public int id_troca { get; set; }
        public String nomeUsuario { get; set; }
        public String nomeFantasia { get; set; }
        public String Descricao { get; set; }
        public String nomeProduto { get; set; }
        public DateTime data { get; set; }
    }

}