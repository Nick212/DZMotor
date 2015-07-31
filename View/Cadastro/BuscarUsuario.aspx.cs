using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DzAnalyzer.Banco;
using DzAnalyzer.Banco.CadastroBD;

namespace DzAnalyzer.View.Cadastro
{
    public partial class BuscarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnBuscar2_Click(object sender, EventArgs e)
        {
            var co = new CadastroOperacoes();

            GridView1.DataSource = co.ListarUsuarios2(TextBox16.Text.ToString());
            GridView1.DataBind();
            DefineColunas();
 
        }

        protected void btnBuscar1_Click(object sender, EventArgs e)
        {
            var co = new CadastroOperacoes();

            GridView1.DataSource = co.ListarUsuarios1(TextBox15.Text.ToString());
            GridView1.DataBind();
            DefineColunas();
        }

        protected void DefineColunas()
        {
            GridView1.HeaderRow.Cells[0].Text = "Nome";
            GridView1.HeaderRow.Cells[1].Text = "CPF   /   CNPJ";
            GridView1.HeaderRow.Cells[2].Text = "Data";
            GridView1.HeaderRow.Cells[3].Text = "E-mail";
            GridView1.HeaderRow.Cells[4].Text = "E-mail Reserva";
            GridView1.HeaderRow.Cells[5].Text = "DOTZ";
            GridView1.HeaderRow.Cells[6].Text = "DDD";
            GridView1.HeaderRow.Cells[7].Text = "Contato";
            GridView1.HeaderRow.Cells[8].Text = "Cidade";
            GridView1.HeaderRow.Cells[9].Text = "CEP";
            GridView1.HeaderRow.Cells[10].Text = "Rua";
            GridView1.HeaderRow.Cells[11].Text = "n°";
            GridView1.HeaderRow.Cells[12].Text = "Estado";
            GridView1.HeaderRow.Cells[13].Text = "Cadastrado Como:";
        }
    }
}