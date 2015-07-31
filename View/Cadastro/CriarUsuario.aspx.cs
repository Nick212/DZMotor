using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.CadastroBD;

namespace DzAnalyzer.View.Cadastro
{
    public partial class CriarUsuario : System.Web.UI.Page
    {
        private CadastroOperacoes cadastrooperacoes = new CadastroOperacoes();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var estados = cadastrooperacoes.ListarEstado();
                ddl_est.DataValueField = "idEstado";
                ddl_est.DataTextField = "nome";
                ddl_est.DataSource = estados;
                ddl_est.DataBind();
                var tipos = cadastrooperacoes.ListarTipo();
                ddl_tipo.DataValueField = "idTipoPessoa";
                ddl_tipo.DataTextField = "descricao";
                ddl_tipo.DataSource = tipos;
                ddl_tipo.DataBind();
            }
        }

        protected void ddl_est_SelectedIndexChanged(object sender, EventArgs e)
        {
            var estadoSelecionado = ddl_est.SelectedValue;
            var cidades = cadastrooperacoes.ListarCidade(estadoSelecionado);
            ddl_cid.DataValueField = "idCidade";
            ddl_cid.DataTextField = "nomeCidade";
            ddl_cid.DataSource = cidades;
            ddl_cid.DataBind();

        }

        public void limparDados()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            string nome = TextBox1.Text;
            string documento = TextBox2.Text;
            DateTime data = DateTime.Parse(TextBox3.Text);
            string email = TextBox4.Text;
            string emailR = TextBox5.Text;
            int tipo = Int32.Parse(ddl_tipo.SelectedValue);
            string nomeEst = ddl_est.SelectedValue;
            string nomeCid = ddl_cid.SelectedValue;
            string cep = TextBox8.Text;
            string rua = TextBox9.Text;
            string numero = TextBox10.Text;
            string ddd = TextBox11.Text;
            string telefone = TextBox12.Text;
            var operacoes = new CadastroOperacoes();

            if (operacoes.VerificaDocumento(documento) == false)
            {
                erroDocumento.Visible = true;
                erroBox.Text = "Seu CPF ou CNPJ já esta cadastro em nossa base de dados, favor entrar em contato com a central de atendimento!!!";
            }
            else
            {
                operacoes.CadastrarUsuario(nome, documento, data, email, emailR, tipo, nomeEst, nomeCid, cep, rua, numero, ddd, telefone);

                limparDados();
                ModalSucesso.Visible = true;
                sucesso.Text = "Cadastro Efetuado com Sucesso!!!\n";
            }
        }

        protected void btnFecharModalAlerta2_Click(object sender, EventArgs e)
        {
            ModalSucesso.Visible = false;

        }

        protected void btnFecharModalAlerta3_Click(object sender, EventArgs e)
        {
            erroDocumento.Visible = false;
        }

        /*protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "validação cpf", "alert('teste');", true);
        }*/

    }
}