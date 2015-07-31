using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Models.Cadastro
{
    public class ConsultaCad
    {
        public int idUsuario { get; set; }
        public string nome { get; set; }
        public string documento { get; set; }
        public DateTime dataUsuario { get; set; }
        public string email { get; set; }
        public string emailReserva { get; set; }
        public int saldo { get; set; }
        public string ddd { get; set; }
        public string telefone { get; set; }
        public string nomeCidade { get; set; }
        public string cep { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string nomeEstado { get; set; }
        public string descricao { get; set; }
    }
}