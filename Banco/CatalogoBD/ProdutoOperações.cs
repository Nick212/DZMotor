using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DzAnalyzer.Catalogo;
using DzAnalyzer.Models.Parceiro;
using DzAnalyzer.Models.Catalogo;


namespace DzAnalyzer.Banco.CatalogoBD
{
    public class ProdutoOperações
    {
        private ConexaoBD conn;
        private List<Produto> produtos = new List<Produto>();

        public int CadastrarProduto(Produto produto)
        {
            using (conn = new ConexaoBD())
            {
                try
                {
                    string comando = @"INSERT INTO PRODUTO
                   (ID_PARCEIRO
                   ,ID_CATEGORIA_SUB
                   ,ID_PRODUTO_STATUS
                   ,NOME_PRODUTO
                   ,QUANTIDADE_PRODUTO
                   ,DESCRICAO_PRODUTO
                   ,MARCA_PRODUTO
                   ,VALOR_PRODUTO
                   ,DOTZ_PRODUTO
                   ,DATA_CRIACAO)
                VALUES
                   (
		           @ID_PARCEIRO,
                   @ID_CATEGORIA_SUB, 
                   @ID_PRODUTO_STATUS, 
                   @NOME_PRODUTO,
                   @QUANTIDADE_PRODUTO, 
                   @DESCRICAO_PRODUTO,
                   @MARCA_PRODUTO,
                   @VALOR_PRODUTO,
                   @DOTZ_PRODUTO,'"
                   + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    conn.openConnection();

                    SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                    cmd.Parameters.Add(new SqlParameter("@ID_PARCEIRO", produto.parceiro.id_parceiro));
                    cmd.Parameters.Add(new SqlParameter("@ID_CATEGORIA_SUB", produto.subCategoria.idSubCategoria));
                    cmd.Parameters.Add(new SqlParameter("@ID_PRODUTO_STATUS", produto.status.idStatus));
                    cmd.Parameters.Add(new SqlParameter("@NOME_PRODUTO", produto.nomeProduto));
                    cmd.Parameters.Add(new SqlParameter("@QUANTIDADE_PRODUTO", produto.quantidadeProduto));
                    cmd.Parameters.Add(new SqlParameter("@DESCRICAO_PRODUTO", produto.descricaoProduto));
                    cmd.Parameters.Add(new SqlParameter("@MARCA_PRODUTO", produto.marcaProduto));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_PRODUTO", produto.valorProduto));
                    cmd.Parameters.Add(new SqlParameter("@DOTZ_PRODUTO", produto.dotzProduto));

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

        public List<ProdutoModelView> ListaProdutoBancoParceiro(string parceiro)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_PARCEIRO = @PARCEIRO
                                ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                cmd.Parameters.Add(new SqlParameter("@PARCEIRO", parceiro));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoSubCategoria(string subcategoria)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB = @SUBCATEGORIA
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@SUBCATEGORIA", subcategoria));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoStatus(string status)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_PRODUTO_STATUS = @STATUS
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@STATUS", status));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoParceiroStatus(string parceiro, string status)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_PRODUTO_STATUS = @STATUS AND P.ID_PARCEIRO = @PARCEIRO
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                cmd.Parameters.Add(new SqlParameter("@PARCEIRO", parceiro));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoParceiroSubCategoria(string parceiro, string subcategoria)
        {

            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB = @SUBCATEGORIA AND P.ID_PARCEIRO = @PARCEIRO
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@SUBCATEGORIA", subcategoria));
                cmd.Parameters.Add(new SqlParameter("@PARCEIRO", parceiro));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoSubCategoriaStatus(string subcategoria, string status)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB = @SUBCATEGORIA AND P.ID_PRODUTO_STATUS = @STATUS
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@SUBCATEGORIA", subcategoria));
                cmd.Parameters.Add(new SqlParameter("@STATUS", status));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }
            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoParceiroSubCategoriaStatus(string parceiro, string subcategoria, string status)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB = @SUBCATEGORIA AND P.ID_PRODUTO_STATUS = @STATUS AND P.ID_PARCEIRO = @PARCEIRO
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@SUBCATEGORIA", subcategoria));
                cmd.Parameters.Add(new SqlParameter("@STATUS", status));
                cmd.Parameters.Add(new SqlParameter("@PARCEIRO", parceiro));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoCategoria(string categoria)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB IN (SELECT ID_CATEGORIA_SUB FROM CATEGORIA_SUB WITH(NOLOCK) WHERE ID_CATEGORIA = @CATEGORIA)  
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@CATEGORIA", categoria));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoCategoriaParceiro(string categoria, string parceiro)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB IN (SELECT ID_CATEGORIA_SUB FROM CATEGORIA_SUB WITH(NOLOCK) WHERE ID_CATEGORIA = @CATEGORIA) 
                                    AND P.ID_PARCEIRO = @PARCEIRO
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@CATEGORIA", categoria));
                cmd.Parameters.Add(new SqlParameter("@PARCEIRO", parceiro));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoCategoriaStatus(string categoria, string status)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB IN (SELECT ID_CATEGORIA_SUB FROM CATEGORIA_SUB WITH(NOLOCK) WHERE ID_CATEGORIA = @CATEGORIA) 
                                    AND P.ID_PRODUTO_STATUS = @STATUS
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@CATEGORIA", categoria));
                cmd.Parameters.Add(new SqlParameter("@STATUS", status));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBancoCategoriaParceiroStatus(string categoria, string parceiro, string status)
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                WHERE P.ID_CATEGORIA_SUB IN (SELECT ID_CATEGORIA_SUB FROM CATEGORIA_SUB WITH(NOLOCK) WHERE ID_CATEGORIA = @CATEGORIA) 
                                    AND P.ID_PARCEIRO = @PARCEIRO
                                    AND P.ID_PRODUTO_STATUS = @STATUS
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());
                cmd.Parameters.Add(new SqlParameter("@CATEGORIA", categoria));
                cmd.Parameters.Add(new SqlParameter("@PARCEIRO", parceiro));
                cmd.Parameters.Add(new SqlParameter("@STATUS", status));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public List<ProdutoModelView> ListaProdutoBanco()
        {
            string comando = @"SELECT TOP 100
                                	P.ID_PRODUTO,
                                	PA.NOME_FANTASIA_PARCEIRO,
                                	C.DESCRICAO_CATEGORIA,
                                	CS.DESCRICAO_CATEGORIA_SUB,
                                	PS.DESCRICAO_PRODUTO_STATUS,
                                	P.NOME_PRODUTO,
                                	P.QUANTIDADE_PRODUTO,
                                	P.DESCRICAO_PRODUTO,
                                	P.MARCA_PRODUTO,
                                	P.VALOR_PRODUTO,
                                	P.DOTZ_PRODUTO,
                                	P.DATA_CRIACAO,
                                	P.DATA_DESATIVACAO,
                                    P.ID_PRODUTO_STATUS
                                FROM PRODUTO P WITH(NOLOCK)
                                INNER JOIN PARCEIRO PA WITH(NOLOCK)
                                	ON P.ID_PARCEIRO = PA.ID_PARCEIRO
                                INNER JOIN CATEGORIA_SUB CS WITH(NOLOCK)
                                	ON P.ID_CATEGORIA_SUB = CS.ID_CATEGORIA_SUB
                                INNER JOIN CATEGORIA C WITH(NOLOCK)
                                	ON CS.ID_CATEGORIA = C.ID_CATEGORIA
                                INNER JOIN PRODUTO_STATUS PS WITH(NOLOCK)
                                	ON P.ID_PRODUTO_STATUS = PS.ID_PRODUTO_STATUS
                                    ORDER BY P.ID_PRODUTO DESC";
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {

                    Produto produto = new Produto();

                    produto.idProduto = rd.GetInt32(0);
                    produto.parceiro.nome_fantasia = rd.GetString(1);
                    produto.subCategoria.categoria.descricaoCategoria = rd.GetString(2);
                    produto.subCategoria.descricaoSubCategoria = rd.GetString(3);
                    produto.status.descricaoStatus = rd.GetString(4);
                    produto.nomeProduto = rd.GetString(5);
                    produto.quantidadeProduto = rd.GetInt32(6);
                    produto.descricaoProduto = rd.GetString(7);
                    produto.marcaProduto = rd.GetString(8);
                    produto.valorProduto = rd.GetDecimal(9);
                    produto.dotzProduto = rd.GetInt32(10);
                    produto.dataCriacao = rd.GetDateTime(11);
                    var data_desativacao = rd.GetSqlDateTime(12);
                    produto.dataDesativacao = data_desativacao.IsNull ? null : (DateTime?)Convert.ToDateTime(data_desativacao.Value);
                    produto.status.idStatus = rd.GetInt32(13);

                    produtos.Add(produto);
                }
                conn.closeConnection();
            }

            var produtoModalView = new ProdutoModelView();

            return produtoModalView.CriarListaProdutoModalView(produtos);
        }

        public int AtualizarProduto(Produto produto)
        {
            using (conn = new ConexaoBD())
            {
                try
                {
                    string comando = @"UPDATE PRODUTO
                                        SET MARCA_PRODUTO = @MARCA,
	                                        ID_PRODUTO_STATUS = @STATUS,
	                                        VALOR_PRODUTO = @VALOR,
	                                        DOTZ_PRODUTO = @DZ,
	                                        QUANTIDADE_PRODUTO = @QUANTIDADE,
	                                        DESCRICAO_PRODUTO = @DESC,
                                            DATA_DESATIVACAO = NULL
                                        WHERE ID_PRODUTO = @ID";

                    conn.openConnection();

                    SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                    cmd.Parameters.Add(new SqlParameter("@MARCA", produto.marcaProduto));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", produto.status.idStatus));
                    cmd.Parameters.Add(new SqlParameter("@VALOR", produto.valorProduto));
                    cmd.Parameters.Add(new SqlParameter("@DZ", produto.dotzProduto));
                    cmd.Parameters.Add(new SqlParameter("@QUANTIDADE", produto.quantidadeProduto));
                    cmd.Parameters.Add(new SqlParameter("@DESC", produto.descricaoProduto));
                    cmd.Parameters.Add(new SqlParameter("@ID", produto.idProduto));

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

        public int AtualizarProdutoDataDesativacao(Produto produto)
        {
            using (conn = new ConexaoBD())
            {
                try
                {
                    string comando = @"UPDATE PRODUTO
                                        SET MARCA_PRODUTO = @MARCA,
	                                        ID_PRODUTO_STATUS = @STATUS,
	                                        VALOR_PRODUTO = @VALOR,
	                                        DOTZ_PRODUTO = @DZ,
	                                        QUANTIDADE_PRODUTO = @QUANTIDADE,
	                                        DESCRICAO_PRODUTO = @DESC,
                                            DATA_DESATIVACAO = getdate()
                                        WHERE ID_PRODUTO = @ID";

                    conn.openConnection();

                    SqlCommand cmd = new SqlCommand(comando, conn.getConnection());

                    cmd.Parameters.Add(new SqlParameter("@MARCA", produto.marcaProduto));
                    cmd.Parameters.Add(new SqlParameter("@STATUS", produto.status.idStatus));
                    cmd.Parameters.Add(new SqlParameter("@VALOR", produto.valorProduto));
                    cmd.Parameters.Add(new SqlParameter("@DZ", produto.dotzProduto));
                    cmd.Parameters.Add(new SqlParameter("@QUANTIDADE", produto.quantidadeProduto));
                    cmd.Parameters.Add(new SqlParameter("@DESC", produto.descricaoProduto));
                    cmd.Parameters.Add(new SqlParameter("@ID", produto.idProduto));

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



