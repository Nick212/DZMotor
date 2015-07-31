using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Fatura
{
    public class Fatura
    {
        public int idFatura { get; set; }
        public string nomeParceiro { get; set; }
        public DateTime dataAbertura { get; set; }
        public DateTime dataFechamento { get; set; }
        public decimal desconto { get; set; }
        public decimal valorFatura { get; set; }
        public string descStatus { get; set; }
    }
}