using DzAnalyzer.Banco.CreditoBD;
using DzAnalyzer.Models.Cadastro;
using DzAnalyzer.Models.Credito;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DzAnalyzer.View.Credito
{
    public partial class Editar : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            /*valor_compra.Text = "";
            parceiro.Text = "";
            data_credito.Text = "";
            erro.Text = "";*/

            if (!IsPostBack)
            
            {
                Credito_Operações crd_banco = new Credito_Operações();

                var categorias = crd_banco.ListaStatus();
                lista_status.DataValueField = "id_status";
                lista_status.DataTextField = "descricao_status";
                lista_status.DataSource = categorias;
                lista_status.DataBind();
                lista_status.Items.Insert(0, new ListItem("Selecione"));
            }
        }

        protected void btn_credito_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                lista_credito.Visible = false;
                alterar.Visible = false;
                txtAlerta.Text = "";

                Credito_Operações crd_banco = new Credito_Operações();

                if (crd_banco.ValidaCredito(Int32.Parse(id_credito.Text)))
                {
                    hf_credito.Value = id_credito.Text;

                    string nome_parceiro = null;

                    Valor_Mecanica valor_mecanica = crd_banco.ListaValor_Mecanica(Int32.Parse(id_credito.Text));

                    alterar.Visible = true;

                    valor_compra.Text = valor_mecanica.valor_compra.ToString();

                    var categorias = crd_banco.ListaMecanica_Editar(Int32.Parse(id_credito.Text), ref nome_parceiro);
                    lista_mecanica.DataValueField = "id_mecanica";
                    lista_mecanica.DataTextField = "desc_mecanica";
                    lista_mecanica.DataSource = categorias;
                    lista_mecanica.DataBind();
                    lista_mecanica.Items.Insert(0, new ListItem("Selecione"));

                    parceiro.Text = nome_parceiro;
                    data_credito.Text = DateTime.Now.ToString();


                    hf_valor.Value = valor_mecanica.valor_compra.ToString();
                    hf_mecanica.Value = valor_mecanica.id_mecanica.ToString();
                    hf_status.Value = valor_mecanica.id_status.ToString();

                }
                else { erros.Add("ID de Crédito não existe"); }
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

                id_credito.Text = "";
                nome.Text = "";
                documento.Text = "";
            }
        }

        protected void btn_nome_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                lista_credito.Visible = false;
                alterar.Visible = false;
                txtAlerta.Text = "";

                Credito_Operações crd_banco = new Credito_Operações();

                List<Credito_Nome> lista = crd_banco.ListaNomes_Editar(nome.Text);

                if (lista.Count > 0)
                {
                    lista_credito.Visible = true;
                    lista_credito.DataSource = lista;
                    lista_credito.DataBind();
                    lista_credito.HeaderRow.Cells[0].Text = "";
                    lista_credito.HeaderRow.Cells[1].Text = "ID do Crédito";
                    lista_credito.HeaderRow.Cells[2].Text = "Nome do Usuário";
                    lista_credito.HeaderRow.Cells[3].Text = "Valor da Compra";
                    lista_credito.HeaderRow.Cells[4].Text = "Documento";
                    lista_credito.HeaderRow.Cells[5].Text = "Valor Dotz";
                }
                else { erros.Add("Usuário não econtrado"); }
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

                id_credito.Text = "";
                nome.Text = "";
                documento.Text = "";
            }

        }

        protected void lista_credito_command(object sender, GridViewCommandEventArgs e)
        {
            lista_credito.Visible = false;

            Credito_Operações crd_banco = new Credito_Operações();
            var id = e.CommandArgument.ToString();

            string nome_parceiro = null;

            hf_credito.Value = id;

            Valor_Mecanica valor_mecanica = crd_banco.ListaValor_Mecanica(Int32.Parse(id));

            alterar.Visible = true;

            valor_compra.Text = valor_mecanica.valor_compra.ToString();

            var categorias = crd_banco.ListaMecanica_Editar(Int32.Parse(id), ref nome_parceiro);
            lista_mecanica.DataValueField = "id_mecanica";
            lista_mecanica.DataTextField = "desc_mecanica";
            lista_mecanica.DataSource = categorias;
            lista_mecanica.DataBind();
            lista_mecanica.Items.Insert(0, new ListItem("Selecione"));

            parceiro.Text = nome_parceiro;
            data_credito.Text = DateTime.Now.ToString();


            hf_valor.Value = valor_mecanica.valor_compra.ToString();
            hf_mecanica.Value = valor_mecanica.id_mecanica.ToString();
            hf_status.Value = valor_mecanica.id_status.ToString();

        }

        protected void btn_editar_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                lista_credito.Visible = false;

                Credito_Operações crd_banco = new Credito_Operações();
                txtAlerta.Text = "";

                if (lista_status.SelectedIndex != 0 || lista_mecanica.SelectedIndex != 0)
                {
                    if ((Int32.Parse(hf_mecanica.Value) != Int32.Parse(lista_mecanica.SelectedValue.ToString()) ||
                        decimal.Parse(hf_valor.Value) != decimal.Parse(valor_compra.Text)) ||
                        Int32.Parse(hf_status.Value) != Int32.Parse(lista_status.SelectedValue))
                    {
                        if (decimal.Parse(valor_compra.Text.Replace(".", ",")) > 0)
                        {
                            ModeloCredito crd = new ModeloCredito();
                            crd.id_mecanica = Int32.Parse(lista_mecanica.SelectedValue.ToString());
                            crd.valor_compra = decimal.Parse(valor_compra.Text);
                            crd.id_status = Int32.Parse(lista_status.SelectedValue.ToString());
                            crd.id_usuario = Credito_Operações.RetornaID(Int32.Parse(hf_credito.Value));
                            crd.id_credito = Int32.Parse(hf_credito.Value);
                            crd.data_credito = DateTime.Parse(data_credito.Text);

                            if (crd_banco.LimpaCriacao(Int32.Parse(hf_credito.Value), decimal.Parse(hf_valor.Value), Int32.Parse(hf_mecanica.Value)) &&
                                crd_banco.UpdateCredito(crd))
                            {
                                erros.Add("Edição realizada!");
                                LimpaTudo();
                            }
                            else
                            {
                                erros.Add("Problemas na Edição do Crédito");
                                LimpaTudo();
                            }
                        }
                        else { erros.Add("Valor da compra inválido!\n"); LimpaTudo(); }
                    }
                    else { erros.Add("Esses valores já estão no banco\n"); }

                }
                else { erros.Add("Selecione Mecânica e Status"); }
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
            }
        }

        protected void btn_documento_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                lista_credito.Visible = false;
                alterar.Visible = false;

                Credito_Operações crd_banco = new Credito_Operações();

                txtAlerta.Text = "";

                string nome_usuario = crd_banco.RetornaNome(double.Parse(documento.Text));

                if (nome_usuario != null)
                {

                    List<Credito_Nome> lista = crd_banco.ListaNomes_Editar(nome_usuario);

                    if (lista.Count > 0)
                    {
                        lista_credito.Visible = true;
                        lista_credito.DataSource = lista;
                        lista_credito.DataBind();
                        lista_credito.HeaderRow.Cells[0].Text = "";
                        lista_credito.HeaderRow.Cells[1].Text = "ID do Crédito";
                        lista_credito.HeaderRow.Cells[2].Text = "Nome do Usuário";
                        lista_credito.HeaderRow.Cells[3].Text = "Valor da Compra";
                    }
                    else { erros.Add("Esse usuário não fez compras"); }
                }
                else { erros.Add("Documento não encontrado"); }
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
                id_credito.Text = "";
                nome.Text = "";
                documento.Text = "";
            }
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

        public void LimpaTudo()
        {
            alterar.Visible = false;
            lista_credito.Visible = false;
            lista_status.Visible = false;
            lista_mecanica.Visible = false;

            hf_credito.Value = string.Empty;
            hf_valor.Value = string.Empty;
            hf_mecanica.Value = string.Empty;
            hf_status.Value = string.Empty;

            lista_mecanica.DataSource = "";
            lista_mecanica.DataBind();
            lista_status.DataSource = "";
            lista_status.DataBind();


        }


    }
}