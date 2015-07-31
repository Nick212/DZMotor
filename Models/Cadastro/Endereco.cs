using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Cadastro
{
    public class Endereco
    {
        public int idEndereco { get; set; }
        public int idUsuario { get; set; }
        public int idCidade { get; set; }
        public int idEstado { get; set; }
        public string cep { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
    }
}