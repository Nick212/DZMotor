using DzAnalyzer.Banco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.Financeiro_BD;
using DzAnalyzer.Models.Fatura;
using DzAnalyzer.Models.Financeiro;

namespace DzAnalyzer.View.Financeiro
{
    public partial class GerenciarFaturas : System.Web.UI.Page
    {
        private string alerta = "";

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                Fatura fatura = new Fatura();
                Financeiro_Operações fo = new Financeiro_Operações();

                ddlParceirosGerenciar.DataValueField = "idParceiro";
                ddlParceirosGerenciar.DataTextField = "nomeFantasiaParceiro";
                ddlParceirosGerenciar.DataSource = fo.ListarParceiros();
                ddlParceirosGerenciar.DataBind();
                ddlParceirosGerenciar.Items.Insert(0, new ListItem("Selecione um Parceiro"));
                ddlParceirosGerenciar.Items.Insert(1, new ListItem("Tudo"));
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            Financeiro_Operações financeiro = new Financeiro_Operações();

            VerificaDDLParceiros();

            if (alerta == "")
            {
                gvGerenciar.Visible = true;

                if (ddlParceirosGerenciar.SelectedIndex != 0)
                {
                    if (ddlParceirosGerenciar.SelectedIndex == 1)
                    {
                        gvGerenciar.DataSource = financeiro.ListaTodasFaturas();
                        gvGerenciar.DataBind();
                    }
                    else
                    {
                        gvGerenciar.DataSource = financeiro.ListaFaturas(Int32.Parse(ddlParceirosGerenciar.SelectedValue));
                        gvGerenciar.DataBind();
                    }
                }
            }
            else
            {
                ModalAlerta.Visible = true;
                txtAlerta.Text = alerta;
            }
        }

        private void VerificaDDLParceiros()
        {
            if (ddlParceirosGerenciar.SelectedIndex == 0 || ddlParceirosGerenciar.SelectedValue == "")
            {
                alerta = alerta + "O campo Parceiro é obrigatório!!\n";
            }
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }
    }
}