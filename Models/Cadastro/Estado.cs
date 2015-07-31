using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Cadastro
{
    public class Estado
    {
        public int idEstado { get; set; }
        public Cidade cidade; 
        public string nome { get; set; }

        public Estado()
        {
            cidade = new Cidade();
        }
    }
}