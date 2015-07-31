using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Extrato
{
    public class ExtratoUsuario
    {

        public int id { get; set; }
        public DateTime data { get; set; }
        public int idCliente { get; set; }
        public int pontos { get; set; }

    }
}