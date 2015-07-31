using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.Troca_DB;

namespace DzAnalyzer.View
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        TrocaOperacoes to = new TrocaOperacoes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var lista = to.listarTodasTrocas();
                gvListaDeTrocas.DataSource = lista;
                gvListaDeTrocas.DataBind();
            }
        }

        protected void btnPesquisarListaDeTrocas_Click(object sender, EventArgs e)
        {
            gvListaDeTrocas.DataSource = null;
            gvListaDeTrocas.DataBind();
            var lista = to.listarTrocasPorCliente(txbDocumentoListaDeTrocas.Text);
            gvListaDeTrocas.DataSource = lista;
            gvListaDeTrocas.DataBind();
            
        }
    }
}