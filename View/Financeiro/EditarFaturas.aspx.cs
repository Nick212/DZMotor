using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.Financeiro_BD;

namespace DzAnalyzer.View.Financeiro
{
    public partial class EditarFaturas : System.Web.UI.Page
    {

        Financeiro_Operações fo = new Financeiro_Operações();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            gvEditar.Visible = true;
            btnSalvar.Visible = true;
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void gvEditar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

 
    }
}