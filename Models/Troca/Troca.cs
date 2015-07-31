using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Catalogo;
using DzAnalyzer.Models.Cadastro;



namespace DzAnalyzer.Models.Troca
{
    public class Troca
    {
        public int IdTroca { get; set; }
        public int Cliente { get; set; }
        public int Fornecedor { get; set; }
        public int Status { get; set; }
        public DateTime Data { get; set; }
        public int Produto { get; set; }
    }

}