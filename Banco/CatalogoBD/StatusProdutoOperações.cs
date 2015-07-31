using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DzAnalyzer.Models.Catalogo;

namespace DzAnalyzer.Banco.CatalogoBD
{
    public class StatusProdutoOperações
    {
        private ConexaoBD conn;

        public List<ProdutoStatus> ListaStatus()
        {
            List<ProdutoStatus> status = new List<ProdutoStatus>();
            string comando = @"SELECT ID_PRODUTO_STATUS,DESCRICAO_PRODUTO_STATUS FROM PRODUTO_STATUS WITH(NOLOCK)";

            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ProdutoStatus s = new ProdutoStatus();
                    s.idStatus = rd.GetInt32(0);
                    s.descricaoStatus = rd.GetString(1);

                    status.Add(s);
                }
                conn.closeConnection();
            }
            return status;
        }

    }
}