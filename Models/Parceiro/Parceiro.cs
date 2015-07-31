using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Parceiro
{
    public class Parceiro
    {
        public int id_parceiro { get; set; }
        public int id_usuario { get; set; }
        public String nome_fantasia { get; set; }
        public int tipo_parceiro { get; set; }
    }
}