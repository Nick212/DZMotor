using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.CreditoBD;
using DzAnalyzer.Models.Credito;
using DzAnalyzer.Models.Cadastro;
using System.Collections;

namespace DzAnalyzer.View.Credito
{
    public partial class Listar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            listagem.Visible = false;
            lista_nome.Visible = false;
        }

        protected void lista_nome_command(object sender, GridViewCommandEventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";

                var id = e.CommandArgument.ToString();

                Credito_Operações crd_banco = new Credito_Operações();

                List<ModeloCredito> lista = crd_banco.ListaID(Int32.Parse(id));

                if (lista.Count > 0)
                {

                    listagem.Visible = true;
                    listagem.DataSource = lista;
                    listagem.DataBind();
                    DefineColunas();
                }
                else
                {
                    erros.Add("ID do Usuário não existe\n");
                }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                if (erros.Count > 0)
                {
                    ModalAlerta.Visible = true;

                    for (int i = 0; i < erros.Count; i++)
                    {
                        txtAlerta.Text = txtAlerta.Text += erros[i];
                    }
                }

                id_usuario.Text = "";
                nome.Text = "";
                opcoesData.SelectedIndex = 0;
                id_credito.Text = "";
            }
        }

        protected void btn_nome_Click(object sender, EventArgs e)
        {

            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";

                lista_nome.Visible = true;
                Credito_Operações crd_banco = new Credito_Operações();

                List<ID_Nome> lista = crd_banco.ListaNomes(nome.Text);

                if (lista.Count > 0)
                {
                    lista_nome.DataSource = lista;
                    lista_nome.DataBind();
                    lista_nome.HeaderRow.Cells[0].Text = "";
                    lista_nome.HeaderRow.Cells[1].Text = "ID do Usuário";
                    lista_nome.HeaderRow.Cells[2].Text = "Nome do Usuário";
                }
                else
                {
                    erros.Add("Nenhum nome cadastrado");
                }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                if (erros.Count > 0)
                {
                    ModalAlerta.Visible = true;

                    for (int i = 0; i < erros.Count; i++)
                    {
                        txtAlerta.Text = txtAlerta.Text += erros[i];
                    }
                }

                id_usuario.Text = "";
                nome.Text = "";
                opcoesData.SelectedIndex = 0;
                id_credito.Text = "";
            }
        }

        protected void btn_tudo_Click(object sender, EventArgs e)
        {

            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";

                Credito_Operações crd_banco = new Credito_Operações();

                List<ModeloCredito> lista = crd_banco.ListaID(Int32.Parse(id_usuario.Text));

                if (lista.Count > 0)
                {
                    listagem.Visible = true;
                    listagem.DataSource = lista;
                    listagem.DataBind();
                    DefineColunas();
                }
                else
                {
                    erros.Add("ID do Usuário não existe");
                }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {

                if (erros.Count > 0)
                {
                    ModalAlerta.Visible = true;

                    for (int i = 0; i < erros.Count; i++)
                    {
                        txtAlerta.Text = txtAlerta.Text += erros[i];
                    }
                }

                id_usuario.Text = "";
                nome.Text = "";
                opcoesData.SelectedIndex = 0;
                id_credito.Text = "";
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";

                Credito_Operações crd_banco = new Credito_Operações();

                if (opcoesData.SelectedValue != "Selecione")
                {
                    listagem.Visible = true;
                    List<ModeloCredito> lista = crd_banco.ListaDatas(DateTime.Now, Int32.Parse(opcoesData.SelectedValue));

                    if (lista.Count > 0)
                    {
                        listagem.DataSource = lista;
                        listagem.DataBind();
                        DefineColunas();
                    }
                    else { erros.Add("Não há créditos para esse período"); }
                }
                else { erros.Add("Selecione o tempo"); }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                if (erros.Count > 0)
                {
                    ModalAlerta.Visible = true;

                    for (int i = 0; i < erros.Count; i++)
                    {
                        txtAlerta.Text = txtAlerta.Text += erros[i];
                    }
                }

                id_usuario.Text = "";
                nome.Text = "";
                opcoesData.SelectedIndex = 0;
                id_credito.Text = "";
            }
        }

        protected void btn_credito_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";

                Credito_Operações crd_banco = new Credito_Operações();

                listagem.Visible = true;
                List<ModeloCredito> lista = crd_banco.ListaCredito(Int32.Parse(id_credito.Text));

                if (lista.Count > 0)
                {
                    listagem.DataSource = lista;
                    listagem.DataBind();
                    DefineColunas();
                }
                else
                {
                    erros.Add("ID de Crédito não existe");
                }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                if (erros.Count > 0)
                {
                    ModalAlerta.Visible = true;

                    for (int i = 0; i < erros.Count; i++)
                    {
                        txtAlerta.Text = txtAlerta.Text += erros[i];
                    }
                }

                id_usuario.Text = "";
                nome.Text = "";
                opcoesData.SelectedIndex = 0;
                id_credito.Text = "";
            }
        }

        protected void DefineColunas()
        {
            listagem.HeaderRow.Cells[0].Text = "ID do Crédito";
            listagem.HeaderRow.Cells[1].Text = "Data da Compra";
            listagem.HeaderRow.Cells[2].Text = "ID do Usuário";
            listagem.HeaderRow.Cells[3].Text = "ID do Status";
            listagem.HeaderRow.Cells[4].Text = "Nota Fiscal";
            listagem.HeaderRow.Cells[5].Text = "ID da Mecânica";
            listagem.HeaderRow.Cells[6].Text = "Data do Crédtio";
            listagem.HeaderRow.Cells[7].Text = "Motivo do Crédtio";
            listagem.HeaderRow.Cells[8].Text = "ID da Fatura";
            listagem.HeaderRow.Cells[9].Text = "Valor da Compra";
            listagem.HeaderRow.Cells[10].Text = "Valor em Dotz";
        }

        protected void lista_nome_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                var id = lista_nome.SelectedIndex.ToString();

                Credito_Operações crd_banco = new Credito_Operações();

                List<ModeloCredito> lista = crd_banco.ListaID(Int32.Parse(id));

                listagem.Visible = true;
                listagem.DataSource = lista;
                listagem.DataBind();
                DefineColunas();
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                if (erros.Count > 0)
                {
                    ModalAlerta.Visible = true;

                    for (int i = 0; i < erros.Count; i++)
                    {
                        txtAlerta.Text = txtAlerta.Text += erros[i];
                    }
                }

                id_usuario.Text = "";
                nome.Text = "";
                opcoesData.SelectedIndex = 0;
                id_credito.Text = "";
            }
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

    }
}