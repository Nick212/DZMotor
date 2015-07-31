using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco;
using System.Data.SqlClient;
using DzAnalyzer.Banco.Financeiro_BD;
using DzAnalyzer.Models.Fatura;
using DzAnalyzer.Models.Financeiro;
using DzAnalyzer.Models.Parceiro;

namespace DzAnalyzer.View.Financeiro
{
    public partial class CriarFaturas : System.Web.UI.Page
    {
        ConexaoBD conn = new ConexaoBD();
        private string alerta = "";
        Fatura fatura = new Fatura();
        Financeiro_Operações fo = new Financeiro_Operações();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlParceiros.DataValueField = "idParceiro";
                ddlParceiros.DataTextField = "nomeFantasiaParceiro";
                ddlParceiros.DataSource = fo.ListarParceiros();
                ddlParceiros.DataBind();
                ddlParceiros.Items.Insert(0, new ListItem("Selecione um Parceiro"));
            }
        }


        protected void btnCriarFatura_Click(object sender, EventArgs e)
        {

           /* VerificaCampoVazio(txbDesconto.Text, "Desconto");*/
            VerificaDDLParceiros();

            if (alerta == "")
            {
                gvFatura.Visible = true;

                List<FaturaModelViewCredito> creditos = new List<FaturaModelViewCredito>();

                FaturaModelViewCredito fatura = new FaturaModelViewCredito();
                fatura = fo.ValoresFatura(ddlParceiros.SelectedValue);

                if (txbDesconto.Text != "")
                {
                    fatura.desconto = decimal.Parse(txbDesconto.Text);
                }
                else
                {
                    fatura.desconto = 0;
                }

                if (fatura.transacoes <= 0)
                {
                    ModalAlerta.Visible = true;
                    txtAlerta.Text = "Não existe creditos para este Parceiro !";
                } 
                else
                {
                    int retorno = fo.InserirFatura(fatura, ddlParceiros.SelectedValue);
                    creditos.Add(fatura);
                    gvFatura.DataSource = creditos;
                    gvFatura.DataBind();
                    ModalAlerta.Visible = true;
                    txtAlerta.Text = "Faturado com sucesso !";
                }
            }
            else 
            {
                ModalAlerta.Visible = true;
                txtAlerta.Text = alerta;
            }
        }
        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

/*        private void VerificaCampoVazio(string valor, string campo)
        {
            if (valor == "")
            {
                alerta = alerta + "O campo " + campo + " é obrigatório!\n";
            }
        }*/

        private void VerificaDDLParceiros()
        {
            if (ddlParceiros.SelectedIndex == 0 || ddlParceiros.SelectedValue == "")
            {
                alerta = alerta + "O campo Parceiro é obrigatório!!\n";
            }
        }
    }
}