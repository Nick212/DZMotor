using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Credito
{
    public class ModeloCredito
    {
        public int id_credito { get; set; }
        public DateTime data_compra { get; set; }
        public int id_usuario { get; set; }
        public int id_status { get; set; }
        public int nota_fiscal { get; set; }
        public int id_mecanica { get; set; }
        public DateTime data_credito { get; set; }
        public String motivo { get; set; }
        public int id_fatura { get; set; }
        public decimal valor_compra { get; set; }
        public int valor_dotz { get; set; }
    }
}