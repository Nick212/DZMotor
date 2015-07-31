using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Credito
{
    public class Credito_Nome
    {
        public int id_credito { get; set; }
        public string nome { get; set; }
        public decimal valor_compra { get; set; }
        public double documento { get; set; }
        public int valor_dotz { get; set; }
    }
}