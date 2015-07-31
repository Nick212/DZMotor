using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using DzAnalyzer.Models.Cadastro;

namespace DzAnalyzer.Banco.CadastroBD
{
    public class EditaContato
    {
        private ConexaoBD conexao;

        public void BuscaDocumento(string documento)
        {
            SqlConnection conn = new SqlConnection(@"Server=172.16.3.236\dev; Initial Catalog=DZPP14-1; Integrated Security= True; Pooling=False");
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;

            comm.CommandText = "SELECT * FROM CADASTRO WHERE DOCUMENTO = @documento";
            comm.Parameters.AddWithValue("@documento", documento);

            SqlDataReader reader = comm.ExecuteReader();
            while (reader.HasRows)
            {
                reader.Read();
                string nome = reader["NOME_USUARIO"] as string;
                string doc = reader["DOCUMENTO"] as string;
                string data = reader["DATA_USUARIO"] as string;
                string mail = reader["EMAIL"] as string;
                string mailR = reader["EMAIL_RESERVA"] as string;
            }

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

            comm.CommandText = "SELECT * FROM ENDERECO WHERE DOCUMENTO = @documento";
            comm.Parameters.AddWithValue("@documento", documento);


        }
    }
}