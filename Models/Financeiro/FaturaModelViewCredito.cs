using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Financeiro
{
    public class FaturaModelViewCredito
    {
        public int transacoes { get; set; }
        public int valorDotz { get; set; }
        public decimal valorReais { get; set; }
        public DateTime dataAbertura { get; set; }
        public DateTime dataFechamento { get; set; }
        public decimal desconto { get; set; }
    }
}