using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DzAnalyzer.Banco.Atendimento
{
    public class Cadastro_Atendimento_Operacoes
    {
        public void CadastrarAtendimento()
        {
            try
            {
                ConexaoBD conn = new ConexaoBD();

                String comando = @"INSERT INTO FROM CHAMADO_ATENDIMENTO_CLIENTE VALUES()";

                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                conn.openConnection();
                cmd.ExecuteNonQuery();
                conn.closeConnection();
                conn.disposeConnection();

            }
            catch (SqlException e)
            {
                Console.WriteLine(" " + e.Errors[0].Number);
            }

        }
    }
}