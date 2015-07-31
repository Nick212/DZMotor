using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DzAnalyzer.Banco;
using DzAnalyzer.Banco.CadastroBD;
using DzAnalyzer.Banco.ParceiroBD;
using DzAnalyzer.Models.Parceiro;

namespace DzAnalyzer.View.Parceiro
{
    public partial class Cadastro_parceiro : System.Web.UI.Page
    {

        private ParceiroOperacoes descricaoTipoParceiro = new ParceiroOperacoes ();

        protected void LimparCampos()
        {
            nome.Text = "";
            nome_fantasia.Text = "";
            cnpj.Text = "";
            data.Text = "";
            cep.Text = "";
            rua.Text = "";
            numero.Text = "";
            email.Text = "";
            emailReserva.Text = "";
            ddd1.Text = "";
            telefone.Text = "";
            ddl_est.Text = "";
            ddl_cid.Text = "";
            TipoDeParceiro.Text = "";
           
        }
        private CadastroOperacoes cadastroUF = new CadastroOperacoes();

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

                TipoDeParceiro.DataValueField = "idTipoParceiro";
                TipoDeParceiro.DataTextField = "descTipoParceiro";
                TipoDeParceiro.DataSource = descricaoTipoParceiro.ListarTipoParceiro();
                TipoDeParceiro.DataBind();
                TipoDeParceiro.Items.Insert(0, new ListItem(""));
                
                
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

       

        public void btnInsereDados_Click(object sender, EventArgs e)
        {
            var razaoSocial = nome.Text;
            var nomeFantasia = nome_fantasia.Text;
            var cnpjUsuario = cnpj.Text;
            var dataFundacao = data.Text;
            var cepEndereco = cep.Text;
            var ruaEndereco = rua.Text;
            var numeroEndereco = numero.Text;
            var emailUsuario = email.Text;
            var emailUsuarioReserva = emailReserva.Text;
            var dddComercial = ddd1.Text;
            var telefoneComercial = telefone.Text;
            

            ConexaoBD conexao = new ConexaoBD();
            conexao.openConnection();


                var buscaCnpj = new SqlCommand(@"SELECT DOCUMENTO FROM CADASTRO WHERE DOCUMENTO = '" + cnpjUsuario + "'", conexao.getConnection());
                var rd = buscaCnpj.ExecuteReader();

                if (!rd.Read())
                {

                    rd.Close();
                    rd.Dispose();

                    if(razaoSocial != "" && cnpjUsuario != "" && dataFundacao != "" && emailUsuario != "" && emailUsuarioReserva != ""
                        && nomeFantasia != "" && cepEndereco != "" && ruaEndereco != "" && numeroEndereco != "" && telefoneComercial != "")
                    {
                        var cmd = new SqlCommand(@"INSERT INTO CADASTRO(NOME_USUARIO, DOCUMENTO, DATA_USUARIO, EMAIL, EMAIL_RESERVA, SALDO, TIPO_PESSOA) VALUES('" + razaoSocial + "'," +
                                                        "'" + cnpjUsuario + "'," +
                                                        "'" + dataFundacao + "'," +
                                                        "'" + emailUsuario + "'," +
                                                        "'" + emailUsuarioReserva + "'," +
                                                        "0," +
                                                        "2 )", conexao.getConnection());

                        cmd.ExecuteNonQuery();
                        conexao.closeConnection();



                        conexao.openConnection();


                        var cmd1 = new SqlCommand(@"INSERT INTO PARCEIRO (ID_USUARIO, NOME_FANTASIA_PARCEIRO, TIPO_PARCEIRO) VALUES ((SELECT ID_USUARIO FROM CADASTRO WITH(NOLOCK)WHERE DOCUMENTO = '"
                                                        + cnpjUsuario + "')," +
                                                    "'" + nomeFantasia + "'," +
                                                    "'" + TipoDeParceiro.SelectedValue + "')", conexao.getConnection());


                        cmd1.ExecuteNonQuery();
                        conexao.closeConnection();



                        conexao.openConnection();


                        var cmd2 = new SqlCommand(@"INSERT INTO ENDERECO (ID_USUARIO, ID_CIDADE, ID_ESTADO, CEP, RUA, NUMERO) VALUES ((SELECT ID_USUARIO FROM CADASTRO WITH (NOLOCK)WHERE DOCUMENTO ='" +
                                                cnpjUsuario + "')," +
                                                "'" + ddl_cid.SelectedValue + "'," +
                                                "'" + ddl_est.SelectedValue + "'," +
                                                "'" + cepEndereco + "'," +
                                                "'" + ruaEndereco + "'," +
                                                "'" + numeroEndereco + "')", conexao.getConnection());

                        cmd2.ExecuteNonQuery();
                        conexao.closeConnection();

                        conexao.openConnection();

                        var cmd3 = new SqlCommand(@"INSERT INTO CONTATO (ID_USUARIO, DDD, TELEFONE) VALUES ((SELECT ID_USUARIO FROM CADASTRO WITH (NOLOCK) WHERE DOCUMENTO = '"
                                                   + cnpjUsuario + "')," +
                                                   dddComercial + "," +
                                                   telefoneComercial + ")", conexao.getConnection());
                        cmd3.ExecuteNonQuery();
                        conexao.closeConnection();


                        LimparCampos();
                        txtAlerta.Text = "CADASTRO EFETUADO COM SUCESSO";
                        ModalAlerta.Visible = true;
                        return;
                    
                    }else if(razaoSocial != "" && cnpjUsuario != "" && dataFundacao != "" && emailUsuario != "" && emailUsuarioReserva == ""
                        && nomeFantasia != "" && cepEndereco != "" && ruaEndereco != "" && numeroEndereco != "" && telefoneComercial != "")
                    {
                        var cmd = new SqlCommand(@"INSERT INTO CADASTRO(NOME_USUARIO, DOCUMENTO, DATA_USUARIO, EMAIL, EMAIL_RESERVA, SALDO, TIPO_PESSOA) VALUES('" + razaoSocial + "'," +
                                                        "'" + cnpjUsuario + "'," +
                                                        "'" + dataFundacao + "'," +
                                                        "'" + emailUsuario + "'," +
                                                        "'NULL'," +
                                                        "0," +
                                                        "2 )", conexao.getConnection());

                        cmd.ExecuteNonQuery();
                        conexao.closeConnection();



                        conexao.openConnection();


                        var cmd1 = new SqlCommand(@"INSERT INTO PARCEIRO (ID_USUARIO, NOME_FANTASIA_PARCEIRO, TIPO_PARCEIRO) VALUES ((SELECT ID_USUARIO FROM CADASTRO WITH(NOLOCK)WHERE DOCUMENTO = '"
                                                        + cnpjUsuario + "')," +
                                                    "'" + nomeFantasia + "'," +
                                                    "'" + TipoDeParceiro.SelectedValue + "')", conexao.getConnection());


                        cmd1.ExecuteNonQuery();
                        conexao.closeConnection();



                        conexao.openConnection();


                        var cmd2 = new SqlCommand(@"INSERT INTO ENDERECO (ID_USUARIO, ID_CIDADE, ID_ESTADO, CEP, RUA, NUMERO) VALUES ((SELECT ID_USUARIO FROM CADASTRO WITH (NOLOCK)WHERE DOCUMENTO ='" +
                                                cnpjUsuario + "')," +
                                                "'" + ddl_cid.SelectedValue + "'," +
                                                "'" + ddl_est.SelectedValue + "'," +
                                                "'" + cepEndereco + "'," +
                                                "'" + ruaEndereco + "'," +
                                                "'" + numeroEndereco + "')", conexao.getConnection());

                        cmd2.ExecuteNonQuery();
                        conexao.closeConnection();

                        conexao.openConnection();

                        var cmd3 = new SqlCommand(@"INSERT INTO CONTATO (ID_USUARIO, DDD, TELEFONE) VALUES ((SELECT ID_USUARIO FROM CADASTRO WITH (NOLOCK) WHERE DOCUMENTO = '"
                                                   + cnpjUsuario + "')," +
                                                   dddComercial + "," +
                                                   telefoneComercial + ")", conexao.getConnection());
                        cmd3.ExecuteNonQuery();
                        conexao.closeConnection();


                        LimparCampos();
                        txtAlerta.Text = "CADASTRO EFETUADO COM SUCESSO";
                        ModalAlerta.Visible = true;
                    }
                        
                }
                    else if (rd.Read())
                {
                    
                    txtAlerta.Text = "O CNPJ " + cnpjUsuario + " JÁ EXISTEM EM NOSSO CADASTRO !!";
                    ModalAlerta.Visible = true;
                    return;
                }
                rd.Close();
                rd.Dispose();
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
