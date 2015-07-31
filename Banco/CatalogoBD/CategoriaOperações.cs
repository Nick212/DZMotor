using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Banco;
using System.Data;
using System.Data.SqlClient;
using DzAnalyzer.Models.Catalogo;


namespace DzAnalyzer.Banco.CatalogoBD
{
    public class CategoriaOperações
    {

        private ConexaoBD conn;

        public List<Categoria> ListaCategoriaBanco()
        {
            List<Categoria> categorias = new List<Categoria>();
            string comando = @"SELECT ID_CATEGORIA,DESCRICAO_CATEGORIA FROM CATEGORIA WITH(NOLOCK) ORDER BY DESCRICAO_CATEGORIA";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Categoria categoria = new Categoria();

                    categoria.idCategoria = rd.GetInt32(0);
                    categoria.descricaoCategoria = rd.GetString(1);

                    categorias.Add(categoria);
                }
                conn.closeConnection();
            }

            return categorias;
        }

        public List<SubCategoria> ListaSubCategoriaBanco(string idCategoria)
        {

            List<SubCategoria> subCategorias = new List<SubCategoria>();
            string comando = @"SELECT ID_CATEGORIA_SUB,DESCRICAO_CATEGORIA_SUB FROM CATEGORIA_SUB WITH(NOLOCK) WHERE ID_CATEGORIA =" + idCategoria + " ORDER BY DESCRICAO_CATEGORIA_SUB";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    SubCategoria subCategoria = new SubCategoria();

                    subCategoria.idSubCategoria = rd.GetInt32(0);
                    subCategoria.descricaoSubCategoria = rd.GetString(1);

                    subCategorias.Add(subCategoria);
                }
                conn.closeConnection();
            }

            return subCategorias;
        }

        public int CadastrarCategoria(SubCategoria subCategoria)
        {
            using (conn = new ConexaoBD())
            {
                conn.openConnection();

                SqlTransaction transacao = conn.getConnection().BeginTransaction("transacao");

                try
                {

                    string comando = @"INSERT INTO CATEGORIA
                                       (DESCRICAO_CATEGORIA)
                                    VALUES
                                       (@DESCRICAO_CATEGORIA)";

                    SqlCommand cmd = new SqlCommand(comando, conn.getConnection(), transacao);
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO_CATEGORIA", subCategoria.categoria.descricaoCategoria));
                    cmd.ExecuteNonQuery();

                    comando = @"INSERT INTO CATEGORIA_SUB 
                                (ID_CATEGORIA,DESCRICAO_CATEGORIA_SUB)
                                VALUES (
		                                (SELECT ID_CATEGORIA FROM CATEGORIA WITH(NOLOCK) WHERE DESCRICAO_CATEGORIA = @DESCRICAO_CATEGORIA),
		                                @DESCRICAO_CATEGORIA_SUB		
		                                )";

                    SqlCommand cmd1 = new SqlCommand(comando, conn.getConnection(), transacao);

                    cmd1.Parameters.Add(new SqlParameter("@DESCRICAO_CATEGORIA", subCategoria.categoria.descricaoCategoria));
                    cmd1.Parameters.Add(new SqlParameter("@DESCRICAO_CATEGORIA_SUB", subCategoria.descricaoSubCategoria));
                    cmd1.ExecuteNonQuery();

                    transacao.Commit();
                    conn.closeConnection();
                    return 0;
                }
                catch (SqlException e)
                {
                    transacao.Rollback();                    
                    return e.Errors[0].Number;
                    throw;
                }
            }
        }

        public int CadastrarSubCategoria(SubCategoria subCategoria)
        {
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                
                try
                {

                    string comando = @"INSERT INTO CATEGORIA_SUB 
                                (ID_CATEGORIA,DESCRICAO_CATEGORIA_SUB)
                                VALUES (
		                                @IDCATEGORIA,
		                                @DESCRICAO_CATEGORIA_SUB		
		                                )";;

                    SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                    cmd.Parameters.Add(new SqlParameter("@IDCATEGORIA", subCategoria.categoria.idCategoria));
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO_CATEGORIA_SUB", subCategoria.descricaoSubCategoria));
                    cmd.ExecuteNonQuery();

                    conn.closeConnection();
                    return 0;
                }
                catch (SqlException e)
                {
                    return e.Errors[0].Number;
                    throw;
                }
            }
        }
    }
}