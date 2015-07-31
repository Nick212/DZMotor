using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.CadastroBD;
using DzAnalyzer.Banco;
using System.Data.SqlClient;

namespace DzAnalyzer.View.Parceiro
{
    public partial class Editar_parceiros : System.Web.UI.Page
    {
        private CadastroOperacoes cadastroUF = new CadastroOperacoes();
        private int idUser;
       
        protected void LimparCampos()
        {
            cnpj_text.Text = "";
            nome_fantasia.Text = "";
            email.Text = "";
            telefone.Text = "";
            cep.Text = "";
            rua.Text = "";
            numero.Text = "";
            estado_text.Text = "";
            cidade_text.Text = "";
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var estados = cadastroUF.ListarEstado();
                ddl_est.DataValueField = "idEstado";
                ddl_est.DataTextField = "nome";
                ddl_est.DataSource = estados;
                ddl_est.DataBind();
                ddl_est.Items.Insert(0, new ListItem(""));
            }
        }

        protected void ddl_est_SelectedIndexChanged(object sender, EventArgs e)
        {
            var estadoSelecionado = ddl_est.SelectedValue;
            var cidades = cadastroUF.ListarCidade(estadoSelecionado);
            ddl_cid.DataValueField = "idCidade";
            ddl_cid.DataTextField = "nomeCidade";
            ddl_cid.DataSource = cidades;
            ddl_cid.DataBind();
            ddl_cid.Items.Insert(0, new ListItem(""));

        }
        protected void BotaoConsultaEdicao_Click(object sender, EventArgs e)
        {
            var cnpj = cnpj_text.Text;

            ConexaoBD conexao = new ConexaoBD();
            conexao.openConnection();

            var cmd = new SqlCommand(@"SELECT P.NOME_FANTASIA_PARCEIRO, CO.DDD, CO.TELEFONE, C.EMAIL, E.CEP, E.RUA, E.NUMERO, ES.NOME_ESTADO, CI.NOME_CIDADE
			                            FROM PARCEIRO P
			                            LEFT JOIN CONTATO CO
			                            ON P.ID_USUARIO = CO.ID_USUARIO
			                            INNER JOIN CADASTRO C
			                            ON P.ID_USUARIO = C.ID_USUARIO
			                            INNER JOIN ENDERECO E
			                            ON P.ID_USUARIO = E.ID_USUARIO
                                        INNER JOIN CIDADE CI
                                        ON E.ID_CIDADE = CI.ID_CIDADE
                                        INNER JOIN ESTADO ES
                                        ON E.ID_ESTADO = ES.ID_ESTADO WHERE DOCUMENTO = '" + cnpj + "'", conexao.getConnection());

            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                nome_fantasia.Text = rd.GetString(0);
                telefone.Text = "(" + rd.GetString(1) + ")" + rd.GetString(2);
                email.Text = rd.GetString(3);
                cep.Text = rd.GetString(4);
                rua.Text = rd.GetString(5);
                numero.Text = rd.GetString(6);
                estado_text.Text = rd.GetString(7);
                cidade_text.Text = rd.GetString(8);

            }
            rd.Close();
            rd.Dispose();
            conexao.closeConnection();
        }
        protected void btnAlteraCadastro_Click(object sender, EventArgs e)
        {
            ConexaoBD conexao = new ConexaoBD();
            conexao.openConnection();

            var nome = nome_fantasia_text.Text;
            var email = email_text.Text;
            var ddd = ddd_text.Text;
            var telefone = telefone_text.Text;
            var cep = cep_text.Text;
            var rua = endereco_text.Text;
            var numero = numero_text.Text;
            var cnpj = cnpj_text.Text;

            var cmd = new SqlCommand(@"SELECT ID_USUARIO FROM CADASTRO 
                            WHERE DOCUMENTO = '" + cnpj + "'", conexao.getConnection());
            var rd = cmd.ExecuteReader();



            if (rd.Read())
            {
                idUser = rd.GetInt32(0);

                rd.Close();
                rd.Dispose();
            }

            conexao.closeConnection();

            conexao.openConnection();
            var cmd1 = new SqlCommand(@"UPDATE PARCEIRO SET NOME_FANTASIA_PARCEIRO = '" + nome + "' WHERE ID_USUARIO = " + idUser, conexao.getConnection());
            cmd1.ExecuteNonQuery();
            conexao.closeConnection();

            conexao.openConnection();
            var cmd2 = new SqlCommand(@"UPDATE CADASTRO SET EMAIL = '" + email + "' WHERE ID_USUARIO = " + idUser, conexao.getConnection());
            cmd2.ExecuteNonQuery();
            conexao.closeConnection();

            conexao.openConnection();
            var cmd3 = new SqlCommand(@"UPDATE CONTATO SET DDD = " + ddd + ", TELEFONE = " + telefone + "WHERE ID_USUARIO = " + idUser, conexao.getConnection());
            cmd3.ExecuteNonQuery();
            conexao.closeConnection();

            conexao.openConnection();
            var cmd4 = new SqlCommand(@"UPDATE ENDERECO SET ID_CIDADE = '" + ddl_cid.SelectedValue + "',ID_ESTADO = '" + ddl_est.SelectedValue + "', CEP = " + cep + ", RUA = '" + rua + "', NUMERO = " + numero +
                "WHERE ID_USUARIO = " + idUser, conexao.getConnection());
            cmd4.ExecuteNonQuery();
            conexao.closeConnection();

            LimparCampos();
            txtAlerta.Text = "DADOS EDITADOS COM SUCESSO !!";
            ModalAlerta.Visible = true;
            return;

        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

        public void btnApagaCampos_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
    }
    
}
