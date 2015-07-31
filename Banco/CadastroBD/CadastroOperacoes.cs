using DzAnalyzer.Models.Cadastro;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DzAnalyzer.Banco.CadastroBD
{
    public class CadastroOperacoes
    {
        private ConexaoBD conexao;

        public List<Estado> ListarEstado()
        {
            List<Estado> estados = new List<Estado>();
            String select = "select * from ESTADO";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Estado estado = new Estado();
                    estado.idEstado = rd.GetInt32(0);
                    estado.nome = rd.GetString(1);

                    estados.Add(estado);
                }
                conexao.closeConnection();
            }
            return estados;
        }

        public List<Cidade> ListarCidade(string idEstado)
        {
            List<Cidade> cidades = new List<Cidade>();
            String select = "select * from Cidade where id_estado=" + idEstado;
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Cidade cidade = new Cidade();
                    cidade.idCidade = rd.GetInt32(0);
                    cidade.nomeCidade = rd.GetString(2);

                    cidades.Add(cidade);
                }
                conexao.closeConnection();
            }
            return cidades;
        }

        public List<TipoPessoa> ListarTipo()
        {
            List<TipoPessoa> tipo = new List<TipoPessoa>();

            String select = "select * from TIPO_PESSOA ";

            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    TipoPessoa tipoPessoa = new TipoPessoa();
                    tipoPessoa.idTipoPessoa = rd.GetInt32(0);
                    tipoPessoa.descricao = rd.GetString(1);

                    tipo.Add(tipoPessoa);
                }
                conexao.closeConnection();
            }
            return tipo;
        }

        /*public void CadastrarUsuario2(string nome, string documento, DateTime data, string email, string emailR, int tipo, string nomeEst, string nomeCid, string cep, string rua, string numero, string ddd, string telefone)
        {
            SqlConnection conn = new SqlConnection(@"Server=172.16.3.236\dev; Initial Catalog=DZPP14-1; Integrated Security= True; Pooling=False");
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;

            comm.CommandText = "INSERT INTO CADASTRO (NOME_USUARIO, DOCUMENTO, DATA_USUARIO, EMAIL, EMAIL_RESERVA, SALDO, TIPO_PESSOA)" +
                                "VALUES(@NOME_USUARIO, @DOCUMENTO, @DATA_USUARIO, @EMAIL, @EMAIL_RESERVA, @SALDO, @TIPO_PESSOA);" +
                                "SELECT @@IDENTITY";

            comm.Parameters.AddWithValue("@NOME_USUARIO", nome);
            comm.Parameters.AddWithValue("@DOCUMENTO", documento);
            comm.Parameters.AddWithValue("@DATA_USUARIO", data.ToString("yyyy-MM-dd"));
            comm.Parameters.AddWithValue("@EMAIL", email);
            comm.Parameters.AddWithValue("@EMAIL_RESERVA", emailR);
            comm.Parameters.AddWithValue("@SALDO", 0);
            comm.Parameters.AddWithValue("@TIPO_PESSOA", tipo);

            conn.Open();
            var id = comm.ExecuteScalar();
            conn.Close();

            comm.CommandText = "INSERT INTO ENDERECO ( ID_USUARIO, ID_CIDADE, ID_ESTADO, CEP, RUA, NUMERO)" +
                    "VALUES(@ID_USUARIO, @ID_CIDADE, @ID_ESTADO, @CEP, @RUA, @NUMERO)";

            comm.Parameters.AddWithValue("@ID_USUARIO", id);
            comm.Parameters.AddWithValue("@ID_CIDADE", nomeCid);
            comm.Parameters.AddWithValue("@ID_ESTADO", nomeEst);
            comm.Parameters.AddWithValue("@CEP", cep);
            comm.Parameters.AddWithValue("@RUA", rua);
            comm.Parameters.AddWithValue("@NUMERO", numero);

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

            comm.CommandText = "INSERT INTO CONTATO (ID_USUARIO, DDD, TELEFONE)" +
                                "VALUES(@IDUSUARIO, @DDD, @TELEFONE)";

            comm.Parameters.AddWithValue("@IDUSUARIO", id);
            comm.Parameters.AddWithValue("@DDD", ddd);
            comm.Parameters.AddWithValue("@TELEFONE", telefone);

            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

        }*/

        public void CadastrarUsuario(string nome, string documento, DateTime data, string email, string emailR, int tipo, string nomeEst, string nomeCid, string cep, string rua, string numero, string ddd, string telefone)
        {
            String insert1 = "INSERT INTO CADASTRO (NOME_USUARIO, DOCUMENTO, DATA_USUARIO, EMAIL, EMAIL_RESERVA, SALDO, TIPO_PESSOA)" +
                                "VALUES(@NOME_USUARIO, @DOCUMENTO, @DATA_USUARIO, @EMAIL, @EMAIL_RESERVA, @SALDO, @TIPO_PESSOA);" +
                                "SELECT @@IDENTITY";

            String insert2 = "INSERT INTO ENDERECO ( ID_USUARIO, ID_CIDADE, ID_ESTADO, CEP, RUA, NUMERO)" +
                                "VALUES(@ID_USUARIO, @ID_CIDADE, @ID_ESTADO, @CEP, @RUA, @NUMERO)";

            String insert3 = "INSERT INTO CONTATO (ID_USUARIO, DDD, TELEFONE)" +
                                "VALUES(@IDUSUARIO, @DDD, @TELEFONE)";

            using(conexao = new ConexaoBD())
            {
            conexao.openConnection();
            SqlCommand cmd = new SqlCommand(insert1, conexao.getConnection());

            cmd.Parameters.AddWithValue("@NOME_USUARIO", nome);
            cmd.Parameters.AddWithValue("@DOCUMENTO", documento);
            cmd.Parameters.AddWithValue("@DATA_USUARIO", data.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@EMAIL", email);
            cmd.Parameters.AddWithValue("@EMAIL_RESERVA", emailR);
            cmd.Parameters.AddWithValue("@SALDO", 0);
            cmd.Parameters.AddWithValue("@TIPO_PESSOA", tipo);

            var id = cmd.ExecuteScalar();

            SqlCommand cmd2 = new SqlCommand(insert2, conexao.getConnection());

            cmd2.Parameters.AddWithValue("@ID_USUARIO", id);
            cmd2.Parameters.AddWithValue("@ID_CIDADE", nomeCid);
            cmd2.Parameters.AddWithValue("@ID_ESTADO", nomeEst);
            cmd2.Parameters.AddWithValue("@CEP", cep);
            cmd2.Parameters.AddWithValue("@RUA", rua);
            cmd2.Parameters.AddWithValue("@NUMERO", numero);

            cmd2.ExecuteNonQuery();

            SqlCommand cmd3 = new SqlCommand(insert3, conexao.getConnection());

            cmd3.Parameters.AddWithValue("@IDUSUARIO", id);
            cmd3.Parameters.AddWithValue("@DDD", ddd);
            cmd3.Parameters.AddWithValue("@TELEFONE", telefone);

            cmd3.ExecuteNonQuery();

            conexao.closeConnection();
                
            }
        }

        public List<ConsultaCad2> ListarUsuarios1(string documento)
        {
            List<ConsultaCad2> usuarios = new List<ConsultaCad2>();

            String select = @"SELECT C.NOME_USUARIO
                                      ,C.DOCUMENTO
                                      ,C.DATA_USUARIO
                                      ,C.EMAIL
                                      ,C.EMAIL_RESERVA
                                      ,C.SALDO
                                      ,E.NOME_ESTADO
                                      ,CI.NOME_CIDADE
                                      ,EN.CEP
                                      ,EN.RUA
                                      ,EN.NUMERO
                                      ,CO.DDD
                                      ,CO.TELEFONE
                                      ,T.DESCRICAO
                                      FROM CADASTRO C
                                      LEFT JOIN CONTATO CO
                                      ON C.ID_USUARIO = CO.ID_USUARIO
                                      INNER JOIN ENDERECO EN
                                      ON C.ID_USUARIO = EN.ID_USUARIO
                                      INNER JOIN CIDADE CI
                                      ON EN.ID_CIDADE = CI.ID_CIDADE
                                      INNER JOIN ESTADO E
                                      ON EN.ID_ESTADO = E.ID_ESTADO
                                      INNER JOIN TIPO_PESSOA T
                                      ON C.TIPO_PESSOA = T.TIPO_PESSOA_ID
                                      WHERE DOCUMENTO LIKE '%' + @DOCUMENTO + '%'";

            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());

                cmd.Parameters.Add(new SqlParameter("@DOCUMENTO", documento));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ConsultaCad2 usuario = new ConsultaCad2();
                    usuario.nome = rd.GetString(0);
                    usuario.documento = rd.GetString(1);
                    usuario.dataUsuario = rd.GetDateTime(2);
                    usuario.email = rd.GetString(3);
                    usuario.emailReserva = rd.IsDBNull(4) ? null : rd.GetString(4);
                    usuario.saldo = rd.GetInt32(5);
                    usuario.nomeEstado = rd.GetString(6);
                    usuario.nomeCidade = rd.GetString(7);
                    usuario.cep = rd.GetString(8);
                    usuario.rua = rd.GetString(9);
                    usuario.numero = rd.GetString(10);
                    usuario.ddd = rd.GetString(11);
                    usuario.telefone = rd.GetString(12);
                    usuario.descricao = rd.GetString(13);

                    usuarios.Add(usuario);
                }
                conexao.closeConnection();
            }
            return usuarios;
        }

        public List<ConsultaCad2> ListarUsuarios2(string nome)
        {
            List<ConsultaCad2> usuarios = new List<ConsultaCad2>();

            String select = @"SELECT C.NOME_USUARIO
                                      ,C.DOCUMENTO
                                      ,C.DATA_USUARIO
                                      ,C.EMAIL
                                      ,C.EMAIL_RESERVA
                                      ,C.SALDO
                                      ,E.NOME_ESTADO
                                      ,CI.NOME_CIDADE
                                      ,EN.CEP
                                      ,EN.RUA
                                      ,EN.NUMERO
                                      ,CO.DDD
                                      ,CO.TELEFONE
                                      ,T.DESCRICAO
                                      FROM CADASTRO C
                                      LEFT JOIN CONTATO CO
                                      ON C.ID_USUARIO = CO.ID_USUARIO
                                      INNER JOIN ENDERECO EN
                                      ON C.ID_USUARIO = EN.ID_USUARIO
                                      INNER JOIN CIDADE CI
                                      ON EN.ID_CIDADE = CI.ID_CIDADE
                                      INNER JOIN ESTADO E
                                      ON EN.ID_ESTADO = E.ID_ESTADO
                                      INNER JOIN TIPO_PESSOA T
                                      ON C.TIPO_PESSOA = T.TIPO_PESSOA_ID
                                      WHERE NOME_USUARIO LIKE '%' + @NOME+ '%'";

            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());

                cmd.Parameters.Add(new SqlParameter("@NOME", nome));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ConsultaCad2 usuario = new ConsultaCad2();
                    usuario.nome = rd.GetString(0);
                    usuario.documento = rd.GetString(1);
                    usuario.dataUsuario = rd.GetDateTime(2);
                    usuario.email = rd.GetString(3);
                    usuario.emailReserva = rd.IsDBNull(4) ? null : rd.GetString(4);
                    usuario.saldo = rd.GetInt32(5);
                    usuario.nomeEstado = rd.GetString(6);
                    usuario.nomeCidade = rd.GetString(7);
                    usuario.cep = rd.GetString(8);
                    usuario.rua = rd.GetString(9);
                    usuario.numero = rd.GetString(10);
                    usuario.ddd = rd.GetString(11);
                    usuario.telefone = rd.GetString(12);
                    usuario.descricao = rd.GetString(13);

                    usuarios.Add(usuario);
                }
                conexao.closeConnection();
            }
            return usuarios;
        }

        public List<ConsultaCad> ListarUsuarios3(string nome)
        {
            List<ConsultaCad> usuarios = new List<ConsultaCad>();

            String select = @"SELECT C.ID_USUARIO
                                      ,C.NOME_USUARIO
                                      ,C.DOCUMENTO
                                      ,C.DATA_USUARIO
                                      ,C.EMAIL
                                      ,C.EMAIL_RESERVA
                                      ,C.SALDO
                                      ,E.NOME_ESTADO
                                      ,CI.NOME_CIDADE
                                      ,EN.CEP
                                      ,EN.RUA
                                      ,EN.NUMERO
                                      ,CO.DDD
                                      ,CO.TELEFONE
                                      ,T.DESCRICAO
                                      FROM CADASTRO C
                                      LEFT JOIN CONTATO CO
                                      ON C.ID_USUARIO = CO.ID_USUARIO
                                      INNER JOIN ENDERECO EN
                                      ON C.ID_USUARIO = EN.ID_USUARIO
                                      INNER JOIN CIDADE CI
                                      ON EN.ID_CIDADE = CI.ID_CIDADE
                                      INNER JOIN ESTADO E
                                      ON EN.ID_ESTADO = E.ID_ESTADO
                                      INNER JOIN TIPO_PESSOA T
                                      ON C.TIPO_PESSOA = T.TIPO_PESSOA_ID
                                      WHERE NOME_USUARIO LIKE '%' + @NOME+ '%'";

            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());

                cmd.Parameters.Add(new SqlParameter("@NOME", nome));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ConsultaCad usuario = new ConsultaCad();
                    usuario.idUsuario = rd.GetInt32(0);
                    usuario.nome = rd.GetString(1);
                    usuario.documento = rd.GetString(2);
                    usuario.dataUsuario = rd.GetDateTime(3);
                    usuario.email = rd.GetString(4);
                    usuario.emailReserva = rd.IsDBNull(5) ? null : rd.GetString(4);
                    usuario.saldo = rd.GetInt32(6);
                    usuario.nomeEstado = rd.GetString(7);
                    usuario.nomeCidade = rd.GetString(8);
                    usuario.cep = rd.GetString(9);
                    usuario.rua = rd.GetString(10);
                    usuario.numero = rd.GetString(11);
                    usuario.ddd = rd.GetString(12);
                    usuario.telefone = rd.GetString(13);
                    usuario.descricao = rd.GetString(14);

                    usuarios.Add(usuario);
                }
                conexao.closeConnection();
            }
            return usuarios;
        }

        public List<ConsultaCad> ListarUsuarios4(string documento)
        {
            List<ConsultaCad> usuarios = new List<ConsultaCad>();

            String select = @"SELECT C.ID_USUARIO
                                      ,C.NOME_USUARIO
                                      ,C.DOCUMENTO
                                      ,C.DATA_USUARIO
                                      ,C.EMAIL
                                      ,C.EMAIL_RESERVA
                                      ,C.SALDO
                                      ,E.NOME_ESTADO
                                      ,CI.NOME_CIDADE
                                      ,EN.CEP
                                      ,EN.RUA
                                      ,EN.NUMERO
                                      ,CO.DDD
                                      ,CO.TELEFONE
                                      ,T.DESCRICAO
                                      FROM CADASTRO C
                                      LEFT JOIN CONTATO CO
                                      ON C.ID_USUARIO = CO.ID_USUARIO
                                      INNER JOIN ENDERECO EN
                                      ON C.ID_USUARIO = EN.ID_USUARIO
                                      INNER JOIN CIDADE CI
                                      ON EN.ID_CIDADE = CI.ID_CIDADE
                                      INNER JOIN ESTADO E
                                      ON EN.ID_ESTADO = E.ID_ESTADO
                                      INNER JOIN TIPO_PESSOA T
                                      ON C.TIPO_PESSOA = T.TIPO_PESSOA_ID
                                      WHERE DOCUMENTO LIKE '%' + @DOCUMENTO + '%'";

            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());

                cmd.Parameters.Add(new SqlParameter("@DOCUMENTO", documento));

                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ConsultaCad usuario = new ConsultaCad();
                    usuario.idUsuario = rd.GetInt32(0);
                    usuario.nome = rd.GetString(1);
                    usuario.documento = rd.GetString(2);
                    usuario.dataUsuario = rd.GetDateTime(3);
                    usuario.email = rd.GetString(4);
                    usuario.emailReserva = rd.IsDBNull(5) ? null : rd.GetString(4);
                    usuario.saldo = rd.GetInt32(6);
                    usuario.nomeEstado = rd.GetString(7);
                    usuario.nomeCidade = rd.GetString(8);
                    usuario.cep = rd.GetString(9);
                    usuario.rua = rd.GetString(10);
                    usuario.numero = rd.GetString(11);
                    usuario.ddd = rd.GetString(12);
                    usuario.telefone = rd.GetString(13);
                    usuario.descricao = rd.GetString(14);

                    usuarios.Add(usuario);
                }
                conexao.closeConnection();
            }
            return usuarios;
        }

        public bool VerificaDocumento( string documento)
        {
            String select = @"SELECT DOCUMENTO FROM CADASTRO WHERE DOCUMENTO = @DOCUMENTO";

            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                cmd.Parameters.Add(new SqlParameter("@DOCUMENTO", documento));

                object resultado = cmd.ExecuteScalar();
                if (resultado == null)
                {
                    return true;
                }
                else if (resultado.ToString() == documento)
                {
                    return false;
                }
                else
                {
                    return false;
                }
                conexao.closeConnection();
            }
        }
    }
}