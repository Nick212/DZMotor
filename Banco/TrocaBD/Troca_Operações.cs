using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Banco;
using DzAnalyzer.Models.Troca;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;
using DzAnalyzer.Catalogo;
using DzAnalyzer.Models.Cadastro;

namespace DzAnalyzer.Banco.Troca_DB
{

    public class TrocaOperacoes
    {
        ConexaoBD conexao = new ConexaoBD();

        /// <summary>
        /// este método insere um registro na tabela TROCA da baze de dados DZPP14-1
        /// </summary>
        /// <param name="troca">Objeto da classe troca que representa a entidade TROCA</param>
        public void Insert(Troca troca)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("INSERT INTO TROCA(DATA_TROCA, ID_CLIENTE, ID_PARCEIRO, ID_STATUS, ID_PRODUTO) VALUES(GETDATE(), @CLIENTE, @PARCEIRO, @STATUS, @PRODUTO)", conexao.getConnection());
            cmd.Parameters.AddWithValue("@CLIENTE", troca.Cliente);
            cmd.Parameters.AddWithValue("@PARCEIRO", troca.Fornecedor);
            cmd.Parameters.AddWithValue("@STATUS", troca.Status);
            cmd.Parameters.AddWithValue("@PRODUTO", troca.Produto);
            cmd.ExecuteNonQuery();
            conexao.closeConnection();
        }

        /// <summary>
        /// Este método deleta um registro na tabela TROCA da base de dados DZPP14-1
        /// </summary>
        /// <param name="troca">Objeto da classe troca que representa a entidade TROCA</param>
        public void Delete(Troca troca)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("DELETE FROM TROCA WHERE ID_TROCA=@TROCA", conexao.getConnection());
            cmd.Parameters.AddWithValue("@TROCA", troca.IdTroca);
            cmd.ExecuteNonQuery();
            conexao.closeConnection();
        }

        /// <summary>
        /// Este método seleciona todos os registros da tabela TROCA da base de dados DZPP14-1
        /// </summary>
        /// <returns>retorna uma lista de todos os registro da base de dados DZPP14-1 da tabela TROCA</returns>
        public List<Troca> SelectAll()
        {
            List<Troca> listaTrocas = new List<Troca>();
            conexao.openConnection();
            Troca troca = new Troca();
            var cmdSelectTrocas = new SqlCommand("SELECT ID_TROCA, DATA_TROCA, ID_CLIENTE, ID_PARCEIRO, ID_STATUS, ID_PRODUTO FROM TROCA", conexao.getConnection());
            var rd = cmdSelectTrocas.ExecuteReader();
            while (rd.Read())
            {
                troca.IdTroca = rd.GetInt32(0);
                troca.Data = rd.GetDateTime(1);
                troca.Cliente = rd.GetInt32(2);
                troca.Fornecedor = rd.GetInt32(3);
                troca.Status = rd.GetInt32(4);
                troca.Produto = rd.GetInt32(5);
                listaTrocas.Add(troca);
            }
            conexao.closeConnection();
            return listaTrocas;
        }

        /// <summary>
        /// Este método seleciona um registro na tabela TROCA da base de dados DZPP14-1 com base no id da troca
        /// </summary>
        /// <param name="id_troca">número inteiro que representa o indice identificador da tabela troca</param>
        /// <returns>Todos os dados do registro que for igual ao campo id_troca </returns>
        public Troca Select(int id_troca)
        {
            conexao.openConnection();
            Troca troca = new Troca();
            var cmdSelectTroca = new SqlCommand("SELECT DATA_TROCA, ID_CLIENTE, ID_PRODUTO, ID_PARCEIRO, ID_STATUS FROM TROCA WHERE ID_TROCA=@TROCA", conexao.getConnection());
            cmdSelectTroca.Parameters.AddWithValue("@TROCA", id_troca);
            var rd = cmdSelectTroca.ExecuteReader();
            while (rd.Read())
            {
                troca.Data = rd.GetDateTime(0);
                troca.Cliente = rd.GetInt32(1);
                troca.Fornecedor = rd.GetInt32(3);
                troca.Produto = rd.GetInt32(2);
                troca.Status = rd.GetInt32(4);
            }

            conexao.closeConnection();
            return troca;
        }

        /// <summary>
        /// este método seleciona um registro na tabela CADATRO na base de dados DZPP14-1 com base no id do cliente
        /// </summary>
        /// <param name="idCliente">id cliente referente ao campo ID_USUARIO da tabela CADASTRO</param>
        /// <returns>Dados necessarios para preenchimento dos campos de informação do cliente na edição de trocas</returns>
        public CadastroUsuario SelectClienteTroca(int idCliente)
        {
            conexao.openConnection();
            CadastroUsuario cliente = new CadastroUsuario();
            var cmd = new SqlCommand("SELECT TIPO_PESSOA, NOME_USUARIO, EMAIL, EMAIL_RESERVA, DOCUMENTO, SALDO FROM CADASTRO WHERE ID_USUARIO = @CLIENTE", conexao.getConnection());
            cmd.Parameters.AddWithValue("@CLIENTE", idCliente);
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                cliente.idTipoPessoa = rd.GetInt32(0);
                cliente.nome = rd.GetString(1);
                cliente.email = rd.GetString(2);
                if (rd[3] != DBNull.Value)
                {
                    cliente.emailReserva = rd.GetString(3);
                }
                cliente.documento = rd.GetString(4);
                cliente.saldo = rd.GetInt32(5);
            }

            conexao.closeConnection();
            return cliente;
        }

        /// <summary>
        /// Este método seleciona um registro na tabela PARCEIROS na base de dados DZPP14-1 com base no id do fornecedor
        /// </summary>
        /// <param name="idfornecedor">id do fornecedor referente ao campo ID_PARCEIRO da tabela PARCEIRO</param>
        /// <returns>Daods necessarios para o preenchimento das informações do fornecedor para edição de trocas</returns>
        public TrocaFornecedor SelectFornecedorTroca(int idfornecedor)
        {
            conexao.openConnection();
            TrocaFornecedor fornecedor = new TrocaFornecedor();
            var cmd = new SqlCommand("SELECT CAD.NOME_USUARIO, CAD.DOCUMENTO, FORNEC.NOME_FANTASIA_PARCEIRO, FORNEC.ID_USUARIO FROM PARCEIRO AS FORNEC JOIN CADASTRO AS CAD ON CAD.ID_USUARIO = FORNEC.ID_USUARIO WHERE ID_PARCEIRO = @FORNECEDOR ", conexao.getConnection());
            cmd.Parameters.AddWithValue("@FORNECEDOR", idfornecedor);
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                fornecedor.CNPJ = rd.GetString(1);
                fornecedor.razaoSocial = rd.GetString(0);
                fornecedor.nomeFornecedor = rd.GetString(2);
            }

            conexao.closeConnection();
            return fornecedor;
        }

        /// <summary>
        ///Este método seleciona um registro na tabela PRODUTOS na base de dados DZPP14-1 com baseno id do produto
        /// </summary>
        /// <param name="idProduto">valor referente ao ID_PRODUTO da tabela PRODUTO</param>
        /// <returns>dados necessarios para o preenchimento das informações do produto da troca para edição </returns>
        public Produto SelectProdutoTroca(int idProduto)
        {
            conexao.openConnection();
            Produto produto = new Produto();
            var cmd = new SqlCommand("SELECT ID_PRODUTO, NOME_PRODUTO, DESCRICAO_PRODUTO, MARCA_PRODUTO, DOTZ_PRODUTO FROM PRODUTO WHERE ID_PRODUTO = @PRODUTO", conexao.getConnection());
            cmd.Parameters.AddWithValue("@PRODUTO", idProduto);
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                produto.idProduto = rd.GetInt32(0);
                produto.nomeProduto = rd.GetString(1);
                produto.descricaoProduto = rd.GetString(2);
                produto.marcaProduto = rd.GetString(3);
                produto.dotzProduto = rd.GetInt32(4);
            }

            conexao.closeConnection();
            return produto;
        }

        /// <summary>
        ///Este método seleciona um conjuunto de registros da tabela PRODUTO na base de dados DZPP14-1 com base no saldo do cliente
        /// </summary>
        /// <param name="saldo">Variavel numérica referente ao saldo do usuario no ato da realização da troca</param>
        /// <returns>Lista de produtos que o usuario pode comprar o seu saldo em dotz</returns>
        public List<Produto> ListaDeProdutosPorSaldo(double saldo)
        {
            List<Produto> produtos = new List<Produto>();
            string comando = @"SELECT	ID_PRODUTO,
		                                NOME_PRODUTO,		                                
		                                DESCRICAO_PRODUTO,
		                                MARCA_PRODUTO,		                                
		                                DOTZ_PRODUTO		                                  
                                FROM PRODUTO (NOLOCK)
                                WHERE DOTZ_PRODUTO < " + saldo;
            conexao.openConnection();
            SqlCommand cmd = new SqlCommand(comando, conexao.getConnection());
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Produto produto = new Produto();

                produto.idProduto = rd.GetInt32(0);
                produto.nomeProduto = rd.GetString(1);
                produto.descricaoProduto = rd.GetString(2);
                produto.marcaProduto = rd.GetString(3);
                produto.dotzProduto = rd.GetInt32(4);

                produtos.Add(produto);
            }
            conexao.closeConnection();
            return produtos;
        }

        /// <summary>
        ///Este método seleciona um registro(se existir) da tabela CADASTRO na base de dados DZPP14-1 com base no documento do cliente e o tipo de pessoa
        /// </summary>
        /// <param name="documento">valor do registro DOCUMENTO na tabela CADASTRO</param>
        /// <param name="tipoPessoa">número utilizado como flag para atribuir o valor de pessoa jurídica ou física</param>
        /// <returns>Um objeto do tipo cadastro para preenchimento dos campos</returns>
        public CadastroUsuario PesquisaCliente(String documento, int tipoPessoa)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("SELECT NOME_USUARIO, SALDO, EMAIL, EMAIL_RESERVA, ID_USUARIO FROM CADASTRO WHERE DOCUMENTO=@DOCUMENTO AND TIPO_PESSOA=@TIPOPESSOA", conexao.getConnection());
            cmd.Parameters.AddWithValue("@DOCUMENTO", documento);
            cmd.Parameters.AddWithValue("@TIPOPESSOA", tipoPessoa);
            var rd = cmd.ExecuteReader();
            CadastroUsuario cliente = new CadastroUsuario();
            if (rd.Read())
            {
                cliente.nome = rd.GetString(0);
                cliente.saldo = rd.GetInt32(1);
                cliente.email = rd.GetString(2);

                if (rd[3] != DBNull.Value)
                {
                    cliente.emailReserva = rd.GetString(3);
                }
                cliente.idUsuario = rd.GetInt32(4);
            }
            conexao.closeConnection();
            return cliente;
        }

        /// <summary>
        /// Este método seleciona um registro da tabela FORNECEDOR da base de dados DZPP14-1 com base no id parceiro na tabela produto
        /// </summary>
        /// <param name="idProduto">Valor reference ao DI_PRODUTO da tabela PRODUTO</param>
        /// <returns>Retorna um objeto tipo TrocaFornecedor contendo os dados necessários para preencher campos de fornecedor na tela</returns>
        public TrocaFornecedor RetornaFornecedor(int idProduto)
        {
            conexao.openConnection();
            TrocaFornecedor fornecedor = new TrocaFornecedor();
            var cmd = new SqlCommand("select cad.NOME_USUARIO, parc.NOME_FANTASIA_PARCEIRO, cad.DOCUMENTO, parc.ID_PARCEIRO from PRODUTO prod inner join PARCEIRO parc  on prod.id_parceiro = parc.id_parceiro inner join CADASTRO cad on parc.id_usuario = cad.id_usuario where prod.ID_PRODUTO = @PRODUTO", conexao.getConnection());
            cmd.Parameters.AddWithValue("@PRODUTO", idProduto);
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                fornecedor.razaoSocial = rd.GetString(0);
                fornecedor.nomeFornecedor = rd.GetString(1);
                fornecedor.CNPJ = rd.GetString(2);
                fornecedor.idUsuario = rd.GetInt32(3);
            }
            conexao.closeConnection();
            return fornecedor;
        }

        /// <summary>
        /// Seleciona o número do ultimo registro inserido na tabela TROCA da base de dados DZPP14-1
        /// </summary>
        /// <returns>O último identificador da tabela TROCA</returns>
        public int RetornaId()
        {
            conexao.openConnection();
            int retorno;
            var cmd = new SqlCommand("SELECT TOP 1 ID_TROCA FROM TROCA ORDER BY ID_TROCA DESC", conexao.getConnection());
            var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                retorno = rd.GetInt32(0);
            }
            else
            {
                retorno = 0;
            }
            conexao.closeConnection();
            return retorno;
        }

        /// <summary>
        /// Este método atualiza/edita uma troca
        /// </summary>
        /// <param name="id_produto">Novo produto a ser inserido</param>
        /// <param name="id_parceiro">Novo fornecedor a ser inserido</param>
        /// <param name="id_troca">Número da troca pra realizar a edição/atualização</param>
        public void AtualizaTroca(int id_produto, int id_parceiro, int id_troca)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("UPDATE TROCA SET ID_PRODUTO=@PRODUTO ,ID_PARCEIRO=@PARCEIRO WHERE ID_TROCA=@TROCA", conexao.getConnection());
            cmd.Parameters.AddWithValue("@PRODUTO", id_produto);
            cmd.Parameters.AddWithValue("@PARCEIRO", id_parceiro);
            cmd.Parameters.AddWithValue("@TROCA", id_troca);
            cmd.ExecuteNonQuery();
            conexao.closeConnection();
        }


        /// <summary>
        /// Este método retorna o id do fornecedor a partir do produto
        /// </summary>
        /// <param name="id_produto">id do produto</param>
        /// <returns>id do fornecedor</returns>
        public int BuscarIdParceiro(int id_produto)
        {
            int retorno;
            conexao.openConnection();
            var cmd = new SqlCommand("SELECT ID_PARCEIRO FROM PRODUTO WHERE ID_PRODUTO=@PRODUTO", conexao.getConnection());
            cmd.Parameters.AddWithValue("@PRODUTO", id_produto);
            var rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                retorno = rd.GetInt32(0);
            }
            else
            {
                retorno = 0;
            }
            conexao.closeConnection();
            return retorno;
        }

        /// <summary>
        /// Este método retorna a lista de todas as trocas registradas na tabela TROCA na base de dados DZPP14-1
        /// </summary>
        /// <returns>Lista contendo todos os registros</returns>
        public List<ListaDeTrocas> listarTodasTrocas()
        {
            List<ListaDeTrocas> listaDeTrocas = new List<ListaDeTrocas>();
            conexao.openConnection();
            var cmd = new SqlCommand("SELECT T.ID_TROCA, CAD.NOME_USUARIO, P.NOME_FANTASIA_PARCEIRO, S.DESCRICAO_STATUS, PROD.NOME_PRODUTO, T.DATA_TROCA FROM TROCA T JOIN CADASTRO CAD ON T.ID_CLIENTE = CAD.ID_USUARIO JOIN PARCEIRO P ON T.ID_PARCEIRO = P.ID_PARCEIRO JOIN STATUS_CREDITO_TROCA S ON T.ID_STATUS = S.ID_STATUS JOIN PRODUTO PROD ON T.ID_PRODUTO = PROD.ID_PRODUTO ", conexao.getConnection());
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ListaDeTrocas troca = new ListaDeTrocas();
                troca.id_troca = rd.GetInt32(0);
                troca.nomeUsuario = rd.GetString(1);
                troca.nomeFantasia = rd.GetString(2);
                troca.Descricao = rd.GetString(3);
                troca.nomeProduto = rd.GetString(4);
                troca.data = rd.GetDateTime(5);
                listaDeTrocas.Add(troca);
            }
            conexao.closeConnection();
            return listaDeTrocas;
        }

        /// <summary>
        /// Este método retorna uma lista de registro da tabela TROCA na base de dados DZPP14-1 com base no documento do cliente
        /// </summary>
        /// <param name="documento">documento que referencia o cliente</param>
        /// <returns>Lista contendo todos os regsitros do cliente</returns>
        public List<ListaDeTrocas> listarTrocasPorCliente(String documento)
        {
            List<ListaDeTrocas> listaDeTrocas = new List<ListaDeTrocas>();
            conexao.openConnection();
            var cmd = new SqlCommand("SELECT T.ID_TROCA, CAD.NOME_USUARIO, P.NOME_FANTASIA_PARCEIRO, S.DESCRICAO_STATUS, PROD.NOME_PRODUTO, T.DATA_TROCA FROM TROCA T JOIN CADASTRO CAD ON T.ID_CLIENTE = CAD.ID_USUARIO JOIN PARCEIRO P ON T.ID_PARCEIRO = P.ID_PARCEIRO JOIN STATUS_CREDITO_TROCA S ON T.ID_STATUS = S.ID_STATUS JOIN PRODUTO PROD ON T.ID_PRODUTO = PROD.ID_PRODUTO WHERE CAD.DOCUMENTO = @DOCUMENTO", conexao.getConnection());
            cmd.Parameters.AddWithValue("@DOCUMENTO", "'"+documento+"'");
            var rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                ListaDeTrocas troca = new ListaDeTrocas();
                troca.id_troca = rd.GetInt32(0);
                troca.nomeUsuario = rd.GetString(1);
                troca.nomeFantasia = rd.GetString(2);
                troca.Descricao = rd.GetString(3);
                troca.nomeProduto = rd.GetString(4);
                troca.data = rd.GetDateTime(5);
                listaDeTrocas.Add(troca);
            }
            conexao.closeConnection();
            return listaDeTrocas;
        }

        public void AtualizaSaldo(int custo, int cliente)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("UPDATE CADASTRO SET SALDO=SALDO-@CUSTO WHERE ID_USUARIO=@CLIENTE", conexao.getConnection());
            cmd.Parameters.AddWithValue("@CUSTO", custo);
            cmd.Parameters.AddWithValue("CLIENTE", cliente);
            cmd.ExecuteNonQuery();
            conexao.closeConnection();
        }

        public int PreçoProduto(int idProduto)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("SELECT DOTZ_PRODUTO FROM PRODUTO WHERE ID_PRODUTO=@PRODUTO", conexao.getConnection());
            cmd.Parameters.AddWithValue("@PRODUTO", idProduto);
            var rd = cmd.ExecuteReader();
            rd.Read();
            int retorno = rd.GetInt32(0);
            conexao.closeConnection();
            return retorno;
        }

        public void AtualizaEstoque(int produto)
        {
            conexao.openConnection();
            var cmd = new SqlCommand("UPDATE PRODUTO SET QUANTIDADE_PRODUTO = QUANTIDADE_PRODUTO-1 WHERE ID_PRODUTO=@PRODUTO ", conexao.getConnection());
            cmd.Parameters.AddWithValue("@PRODUTO", produto);
            cmd.ExecuteNonQuery();
            conexao.closeConnection();
        }
    
    }
}
/*
SELECT T.ID_TROCA, CAD.NOME_USUARIO, P.NOME_FANTASIA_PARCEIRO, S.DESCRICAO_STATUS, PROD.NOME_PRODUTO, T.DATA_TROCA 
FROM TROCA T JOIN CADASTRO CAD ON T.ID_CLIENTE = CAD.ID_USUARIO
JOIN PARCEIRO P ON T.ID_PARCEIRO = P.ID_PARCEIRO
JOIN STATUS_CREDITO_TROCA S ON T.ID_STATUS = S.ID_STATUS
JOIN PRODUTO PROD ON T.ID_PRODUTO = PROD.ID_PRODUTO
WHERE CAD.DOCUMENTO = '32345612365'
*/