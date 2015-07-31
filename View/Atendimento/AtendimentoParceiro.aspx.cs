using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DzAnalyzer.Banco;
using DzAnalyzer.Banco.Atendimento;
using DzAnalyzer.Models.Atendimento;

namespace DzAnalyzer.View.Atendimento
{
    public partial class AtendimentoParceiro : System.Web.UI.Page
    {
        //Instancia do Banco- Atendimento para resgatar as informações do tipo atendimento não esquecendo de importar a classe no using
        private Atendimento_Operacoes atendTipoAtendimento = new Atendimento_Operacoes();


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var tipo = atendTipoAtendimento.ListarTipo();
                //tipoAtendimento.DataValueField = "idTipoAtendimento";
                tipoAtendimento.DataTextField = "tipoAtendimento";
                tipoAtendimento.DataSource = tipo;
                tipoAtendimento.DataBind();

               NChamadoCliente_Parceiro number = new NChamadoCliente_Parceiro();
               number = atendTipoAtendimento.NChamadoParceiro();

               nChamado.Text = number.id_n_chamadoParceiro.ToString();

            }

            //Hora instantânea
            dataAberturaChamado.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
          

        }



        protected void btnConsulta_Click(object sender, EventArgs e)
        {

            //Consultar CNPJ
            var cnpj = txtCNPJ.Text;

            if (cnpj.Length == 14)
            {

                ConexaoBD conn = new ConexaoBD();
                conn.openConnection();

                var cmd = new SqlCommand("SELECT ID_USUARIO, NOME_USUARIO,DATA_USUARIO FROM CADASTRO WHERE DOCUMENTO ='" + cnpj + "'", conn.getConnection());
                var rd = cmd.ExecuteReader();

                DateTime data;
                Int32 idParc;

                if (rd.Read())
                {
                    idParc = rd.GetInt32(0);
                    razaoSocial.Text = rd.GetString(1);
                    data = rd.GetDateTime(2);
                    dataFundacao.Text = data.ToString("dd/MM/yyyy");
                    idUsuario.Text = idParc.ToString();
                }

                conn.closeConnection();

            }
            else {
                txtAlerta.Text = "Usuário não Encontrado !!!";
                ModalAlerta.Visible = true;
            }
        }

        protected void btnLimparDados_Click(object sender, EventArgs e)
        {
            txtCNPJ.Text = "";
            razaoSocial.Text = "";
            tipoAtendimento.Text = "--Selecione o Tipo--";
            dataFundacao.Text = "";
            descAtendimento.Text = "";
        }

        protected void btnCadastrarChamadoParceiro_Click(object sender, EventArgs e)
        {
            if (tipoAtendimento.Text != "--Selecione o Tipo--" && descAtendimento.Text != "" && txtCNPJ.Text.Length == 14)
            {
                try
                {
                    ConexaoBD conn = new ConexaoBD();
                    conn.openConnection();
                    Int16 status = 1;

                    var cmd = new SqlCommand("INSERT INTO CHAMADO_ATENDIMENTO_PARCEIRO (ID_STATUS, CNPJ, TIPO_ATENDIMENTO, DESCRICAO_CHAMADO, DATA_ABERTURA_CHAMADO)VALUES (" +
                        status + ",'" + txtCNPJ.Text + "','" + tipoAtendimento.Text + "','" + descAtendimento.Text + "','" + dataAberturaChamado.Text + "')", conn.getConnection());
                    cmd.ExecuteNonQuery();

                    conn.closeConnection();
                }
                catch (Exception i)
                {

                }
                finally
                {
                    txtCNPJ.Text = "";
                    razaoSocial.Text = "";
                    tipoAtendimento.Text = "--Selecione o Tipo--";
                    dataFundacao.Text = "";
                    descAtendimento.Text = "";

                    txtAlerta.Text = "Chamado Cadastrado com Sucesso !!!";
                    ModalAlerta.Visible = true;
                }
            }
            else
            {
                txtAlerta.Text = "Erro ao Cadastrar, Verifique as Informações digitadas !!!";
                ModalAlerta.Visible = true;
            }
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }
    }
}