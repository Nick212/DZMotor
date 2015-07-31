using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.Atendimento;
using DzAnalyzer.Models.Atendimento;

namespace DzAnalyzer.View.Atendimento
{
    public partial class ConsultarChamado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DefineColunas()
        {
            GridView1.HeaderRow.Cells[0].Text = "Nº_Chamado";
            GridView1.HeaderRow.Cells[1].Text = "CPF   /   CNPJ";
            GridView1.HeaderRow.Cells[2].Text = "Nome   /   Empresa";
            GridView1.HeaderRow.Cells[3].Text = "Tipo   Atendimento";
            GridView1.HeaderRow.Cells[4].Text = "Descrição           Atendimento";
            GridView1.HeaderRow.Cells[5].Text = "Data   Abertura   Chamado";
            GridView1.HeaderRow.Cells[6].Text = "Data   Fechamento   Chamado";
            GridView1.HeaderRow.Cells[7].Text = "Status";
        }

        protected void btnConsultarChamado_Click(object sender, EventArgs e)
        {
            if (cpfUsuario.Text == "" || nChamado.Text == "")
            {
                var atOper = new Atendimento_Operacoes();

                GridView1.DataSource = atOper.ListaChamadoCliente();
                GridView1.DataBind();
                DefineColunas();
            }
            else if (cpfUsuario.Text != "" && cpfUsuario.Text.Length >= 11 && cpfUsuario.Text.Length <= 14)
            {
                var atOper = new Atendimento_Operacoes();

                GridView1.DataSource = atOper.ListaChamadoCliente();
                GridView1.DataBind();
                DefineColunas();
            }
        }

        protected void btnConsultarChamado_Click_Parceiro(object sender, EventArgs e)
        {
            if (cpfUsuario.Text == "" || nChamado.Text == "")
            {
                var atOper = new Atendimento_Operacoes();

                GridView1.DataSource = atOper.ListaChamadoParceiro();
                GridView1.DataBind();
                DefineColunas();
            }
            else if (cpfUsuario.Text != "" && cpfUsuario.Text.Length >= 11 && cpfUsuario.Text.Length <= 14)
            {
                var atOper = new Atendimento_Operacoes();

                GridView1.DataSource = atOper.ListaChamadoParceiro();
                GridView1.DataBind();
                DefineColunas();
            }
        }
       
    }
}