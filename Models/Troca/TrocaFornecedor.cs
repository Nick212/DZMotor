using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Troca
{
    public class TrocaFornecedor
    {

        public String nomeFornecedor { get; set; }
        public String razaoSocial { get; set; }
        public String CNPJ { get; set; }
        public int idUsuario { get; set; }


        public TrocaFornecedor()
        {

        }
    }
}