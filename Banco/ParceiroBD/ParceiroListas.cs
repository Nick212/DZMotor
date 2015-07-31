using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Models.Parceiro;
using DzAnalyzer.Banco;
using System.Data.SqlClient;
using DzAnalyzer.Models.Cadastro;

namespace DzAnalyzer.Banco.ParceiroBD
{
    public class ParceiroOperacoes
    {
        private ConexaoBD conexao;

        public List<TipoParceiro> ListarTipoParceiro()
        {
            List<TipoParceiro> tipoParceiro = new List<TipoParceiro>();
            String select = "SELECT * FROM TIPO_PARCEIRO";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    TipoParceiro tipos = new TipoParceiro();

                    tipos.idTipoParceiro = rd.GetInt32(0);
                    tipos.descTipoParceiro = rd.GetString(1);

                    tipoParceiro.Add(tipos);
                }
                conexao.closeConnection();
            }
            return tipoParceiro;
        }

        public List<Parceiro> ListarParceiros()
        {
            List<Parceiro> parceiros = new List<Parceiro>();
            string consulta = "SELECT ID_PARCEIRO, ID_USUARIO, NOME_FANTASIA_PARCEIRO, TIPO_PARCEIRO FROM PARCEIRO ORDER BY NOME_FANTASIA_PARCEIRO";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(consulta, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Parceiro parceiro = new Parceiro();
                    parceiro.id_parceiro = rd.GetInt32(0);
                    parceiro.id_usuario = rd.GetInt32(1);
                    parceiro.nome_fantasia = rd.GetString(2);
                    parceiro.tipo_parceiro = rd.GetInt32(3);
                    parceiros.Add(parceiro);
                }
                conexao.closeConnection();
            }
            return parceiros;
        }
    }
}

        /// <summary>
        /// esse médtodo insere as informações abaixo na tabela CADASTRO
        /// </summary>
        /// <param name="parceiro"></param>
        /// <returns></returns>
        /*public void CadastrarParceiro(string nome_usuario, string nome_fantasia, string documento, DateTime data_fundacao, string cep, string rua_endereco, string numero_endereco, string estado_endereco,
            string cidade_endereco, string email, string email_reserva, string ddd, string telefone, string tipo_parceiro)
        {
            {
                SqlConnection conexao = new SqlConnection(@"Server=172.16.3.236\dev; Initial Catalog=DZPP14-1; Integrated Security= True; Pooling=False");
                SqlCommand conexao_conexao = new SqlCommand();
                conexao_conexao.Connection = conexao;

                conexao_conexao.CommandText = "@INSER INTO CADASTRO (NOME_USUARIO, DOCUMENTO, DATA_USUARIO, EMAIL, EMAIL_RESERVA, SALDO, TIPO_PESSOA) VALUES (@NOME, @DOCUMENTO, @DATA, @EMAIL, @EMAIL_RESERVA, @SALDO, @TIPO); SELECT @@IDENTITY";

                conexao_conexao.Parameters.AddWithValue("@NOME", nome_usuario);
                conexao_conexao.Parameters.AddWithValue("@DOCUMENTO", documento);
                conexao_conexao.Parameters.AddWithValue("@DATA", data_fundacao.ToString("yyyy-MM-dd"));
                conexao_conexao.Parameters.AddWithValue("@EMAIL", email);
                conexao_conexao.Parameters.AddWithValue("@EMAIL_RESERVA", email_reserva);
                conexao_conexao.Parameters.AddWithValue("@SALDO", 0);
                conexao_conexao.Parameters.AddWithValue("@TIPO", 2);

                conexao.Open();
                var id_usuario = conexao_conexao.ExecuteScalar();
                conexao.Close();


                conexao_conexao.CommandText = "INSERT INTO PARCEIRO (ID_USUARIO, NOME_FANTASIA, TIPO_PARCEIRO) VALUES (@ID_USUARIO, @NOME_FANTASIA, @TIPO_PARCEIRO)";

                conexao_conexao.Parameters.AddWithValue("@ID_USUARIO", id_usuario);
                conexao_conexao.Parameters.AddWithValue("@NOME_FANTASIA", nome_fantasia);
                conexao_conexao.Parameters.AddWithValue("@TIPO_PARCEIRO", tipo_parceiro);

                conexao.Open();
                conexao_conexao.ExecuteNonQuery();
                conexao.Close();


                conexao_conexao.CommandText = "INSERT INTO ENDERECO (ID_USUARIO, ID_CIDADE, ID_ESTADO, CEP, RUA, NUMERO) VALUES (@ID_USUARIO, @ID_CIDADE, @ID_ESTADO, @CEP, @RUA, @NUMERO)";

                conexao_conexao.Parameters.AddWithValue("@ID_USUARIO", id_usuario);
                conexao_conexao.Parameters.AddWithValue("@ID_CIDADE", cidade_endereco);
                conexao_conexao.Parameters.AddWithValue("@ID_ESTADO", estado_endereco);
                conexao_conexao.Parameters.AddWithValue("@CEP", cep);
                conexao_conexao.Parameters.AddWithValue("@RUA", rua_endereco);
                conexao_conexao.Parameters.AddWithValue("@NUMERO", numero_endereco);

                conexao.Open();
                conexao_conexao.ExecuteNonQuery();
                conexao.Close();


                conexao_conexao.CommandText = "INSERT INTO CONTATO (ID_USUARIO, DDD, TELEFONE) VALUES (@ID_USUARIO, @DDD, @TELEFONE)";

                conexao_conexao.Parameters.AddWithValue("@ID_USUARIO", id_usuario);
                conexao_conexao.Parameters.AddWithValue("@DDD", ddd);
                conexao_conexao.Parameters.AddWithValue("@TELEFONE", telefone);

                conexao.Open();
                conexao_conexao.ExecuteNonQuery();
                conexao.Close();






                string sql = @"INSERT INTO CADASTRO (NOME_USUARIO, DOCUMENTO, DATA_USUARIO, EMAIL, EMAIL_RESERVA, SALDO, TIPO_PESSOA)
                         VALUES (@NOME, @DOCUMENTO, @DATA_USUARIO, @EMAIL, NULL, 0, 2)";
            
            conexao.openConnection();

            SqlCommand cmd = new SqlCommand(sql, conexao.getConnection());

            cmd.Parameters.Add(new SqlParameter("@NOME", cadastro.nome));
            cmd.Parameters.Add(new SqlParameter("@DOCUMENTO", cadastro.documento));
            cmd.Parameters.Add(new SqlParameter("DATA_USUARIO", cadastro.dataUsuario));
            cmd.Parameters.Add(new SqlParameter("@EMAIL", cadastro.email));
            cmd.Parameters.Add(new SqlParameter("NULL", cadastro.emailReserva));
            cmd.Parameters.Add(new SqlParameter("0", cadastro.saldo));
                cmd.Parameters.Add(new SqlParameter("2", cadastro.idTipoPessoa));



            cmd.ExecuteNonQuery();
            conexao.closeConnection();

            
        }
        
        
        
            CadastroUsuario cadastro = new CadastroUsuario();
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /* public Cadastro ListaParceiros (String documento)
        {
            Cadastro parceiros = new Cadastro();
            String comando = @"SELECT NOME_FANTASIA_PARCEIRO"
         }

        }
}
}

*/