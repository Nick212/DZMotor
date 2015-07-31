using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Cadastro
{
    public class CadastroUsuario
    {
        public int idUsuario { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public DateTime dataUsuario { get; set; }
        public string email { get; set; }
        public string emailReserva { get; set; }
        public int saldo { get; set; }
        public int idTipoPessoa { get; set; }
    }

}