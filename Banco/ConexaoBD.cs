using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DzAnalyzer.Banco
{
    public class ConexaoBD : IDisposable
    {
        String strConn = ConfigurationManager.ConnectionStrings["conexao"].ConnectionString;
        SqlConnection conn;

        public ConexaoBD()
        {
            conn = new SqlConnection(strConn);
        }
        public SqlConnection getConnection()
        {
            return conn;
        }

        public void openConnection()
        {

            conn.Open();
        }

        public void closeConnection()
        {
            conn.Close();
        }

        public void disposeConnection()
        {
            conn.Dispose();
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }
}