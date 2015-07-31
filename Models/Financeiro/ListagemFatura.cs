using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Financeiro
{
    public class ListagemFatura
    {
        public int idFatura { get; set; }
        public string nomeFantasiaParceiro { get; set; }
        public DateTime dataAbertura { get; set; }
        public DateTime dataFechamento { get; set; }
        public double desconto { get; set; }
        public double valorFatura { get; set; }
        public string descricao { get; set; }
    }
}