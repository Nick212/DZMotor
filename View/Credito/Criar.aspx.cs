using DzAnalyzer.Banco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Models.Credito;
using DzAnalyzer.Models.Cadastro;
using DzAnalyzer.Banco.CreditoBD;
using System.Collections;

namespace DzAnalyzer.View.Credito
{
    public partial class Criar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                selecionaNomes.Visible = false;

                id_usuario.Text = "";
                nome_usuario.Text = "";
                documento.Text = "";

                Credito_Operações crd_banco = new Credito_Operações();

                var categorias = crd_banco.ListaStatus();
                lista_status.DataValueField = "id_status";
                lista_status.DataTextField = "descricao_status";
                lista_status.DataSource = categorias;
                lista_status.DataBind();
                lista_status.Items.Insert(0, new ListItem("Selecione"));

                var parceiro = crd_banco.ListaParceiro();
                lista_parceiro.DataValueField = "id_parceiro";
                lista_parceiro.DataTextField = "nome_fantasia";
                lista_parceiro.DataSource = parceiro;
                lista_parceiro.DataBind();
                lista_parceiro.Items.Insert(0, new ListItem("Selecione"));
            }
        }

        protected void btn_doc_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";
                formulario.Visible = false;

                if (btn_documento.Text != "")
                {
                    Credito_Operações crd_banco = new Credito_Operações();

                    var id = crd_banco.ValidaDocumento(double.Parse(btn_documento.Text));

                    if (id > 0)
                    {
                        ID_Nome id_nome = crd_banco.RetornaID_Nome(id);
                        if (id_nome != null)
                        {
                            formulario.Visible = true;
                            id_usuario.Text = id_nome.id_usuario.ToString();
                            nome_usuario.Text = id_nome.nome;
                            documento.Text = id_nome.documento.ToString();
                            data_credito.Text = DateTime.Now.ToString();
                        }
                    }
                    else
                    {
                        erros.Add("Documento não cadastrado");
                    }
                    btn_id_usuario.Text = "";
                    btn_documento.Text = "";
                    nome.Text = "";
                }
                else { erros.Add("Digite um documento"); }
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

        protected void btn_id_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";
                formulario.Visible = false;

                if (btn_id_usuario.Text != "")
                {
                    Credito_Operações crd_banco = new Credito_Operações();
                    ID_Nome id_nome = crd_banco.RetornaID_Nome(Int32.Parse(btn_id_usuario.Text));
                    if (id_nome != null)
                    {
                        formulario.Visible = true;
                        id_usuario.Text = id_nome.id_usuario.ToString();
                        nome_usuario.Text = id_nome.nome;
                        documento.Text = id_nome.documento.ToString();
                        data_credito.Text = DateTime.Now.ToString();
                    }
                    else { erros.Add("Usuário não cadastrado"); }
                }
                else { erros.Add("Digite um ID de Usuário"); }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                btn_id_usuario.Text = "";
                btn_documento.Text = "";
                nome.Text = "";

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

        protected void btn_nome_Click(object sender, EventArgs e)
        {
            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";
                formulario.Visible = false;

                if (nome.Text != "")
                {
                    selecionaNomes.Visible = true;
                    Credito_Operações crd_banco = new Credito_Operações();

                    List<ID_Nome> lista = crd_banco.ListaNomes(nome.Text);

                    if (lista.Count > 0)
                    {
                        selecionaNomes.DataSource = lista;
                        selecionaNomes.DataBind();
                        selecionaNomes.HeaderRow.Cells[0].Text = "";
                        selecionaNomes.HeaderRow.Cells[1].Text = "ID do Usuário";
                        selecionaNomes.HeaderRow.Cells[2].Text = "Nome do Usuário";
                        selecionaNomes.HeaderRow.Cells[3].Text = "Documento";
                    }
                    else
                    {
                        erros.Add("Nenhum nome cadastrado\n");
                    }
                }
                else { erros.Add("Digite um nome\n"); }
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

                btn_id_usuario.Text = "";
                btn_documento.Text = "";
                nome.Text = "";
            }
        }

        protected void selecionaNomes_command(object sender, GridViewCommandEventArgs e)
        {

            ArrayList erros = new ArrayList();

            try
            {
                txtAlerta.Text = "";
                formulario.Visible = false;
                var id = e.CommandArgument.ToString();

                formulario.Visible = true;

                Credito_Operações crd_banco = new Credito_Operações();
                ID_Nome id_nome = crd_banco.RetornaID_Nome(Int32.Parse(id));
                if (id_nome != null)
                {
                    selecionaNomes.Visible = false;
                    formulario.Visible = true;
                    id_usuario.Text = id_nome.id_usuario.ToString();
                    nome_usuario.Text = id_nome.nome;
                    documento.Text = id_nome.documento.ToString();
                    data_credito.Text = DateTime.Now.ToString();
                }
                else { erros.Add("Esse usuário não tem transações"); }
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }
            finally
            {
                btn_id_usuario.Text = "";
                btn_documento.Text = "";
                nome.Text = "";

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

        protected void btn_credito_Click(object sender, EventArgs e)
        {

            ArrayList erros = new ArrayList();

            try
            {

                txtAlerta.Text = "";

                ModeloCredito crd = new ModeloCredito();
                Credito_Operações crd_banco = new Credito_Operações();

                if (data_compra.Text != "" &&
                    motivo.Text != "" &&
                    nota_fiscal.Text != "" &&
                    valor_compra.Text != "" &&
                    motivo.Text != "")
                {
                    var teste = decimal.Parse(valor_compra.Text.Replace(".", ","));
                    if (!(teste >= 0))
                    {
                        erros.Add("Valor da Compra não pode ser menor do que zero\n");
                    }
                    if (DateTime.Parse(data_compra.Text) > DateTime.Parse(data_credito.Text))
                    {
                        erros.Add("Data de credito nao pode ser menor que data de compra\n");
                    }

                    if (DateTime.Parse(data_compra.Text) > DateTime.Now)
                    {
                        erros.Add("Data de compra não pode ser maior do que a atual\n");
                    }
                    if (lista_parceiro.SelectedIndex == 0)
                    {
                        erros.Add("Selecione um parceiro, depois selecione uma mecânica\n");
                    }
                    if (lista_status.SelectedIndex == 0)
                    {
                        erros.Add("Selecione um Status\n");
                    }

                    if (teste >= 0 && DateTime.Parse(data_compra.Text) < DateTime.Parse(data_credito.Text) &&
                        lista_parceiro.SelectedIndex != 0 && lista_status.SelectedIndex != 0 &&
                        DateTime.Parse(data_compra.Text) < DateTime.Now)
                    {
                        crd.id_mecanica = Int32.Parse(lista_mecanica.SelectedValue);
                        crd.id_status = Int32.Parse(lista_status.SelectedValue);
                        crd.motivo = motivo.Text;
                        crd.nota_fiscal = Int32.Parse(nota_fiscal.Text);
                        crd.valor_compra = teste;
                        crd.data_compra = DateTime.Parse(data_compra.Text);
                        crd.data_credito = DateTime.Now;
                        crd.id_usuario = Int32.Parse(id_usuario.Text);

                        int id_credito = 0;
                        int valor_credito = 0;

                        if (crd_banco.FazCredito(crd, ref id_credito, ref valor_credito))
                        {
                            LimpaCampos();
                            erros.Add("Crédito inserido com sucesso! ID da Transação: " + id_credito + " Valor Creditado: " + valor_credito);
                        }
                        else
                        {
                            erros.Add("Problemas com a conexão, tente novamente\n");
                        }
                    }
                }
                else
                {
                    erros.Add("Preencha todos os campos!\n");
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
            }
        }

        public void LimpaCampos()
        {
            txtAlerta.Text = "";
            formulario.Visible = false;
            data_credito.Text = "";
            data_compra.Text = "";
            motivo.Text = "";
            nota_fiscal.Text = "";
            valor_compra.Text = "";
            id_usuario.Text = "";
            lista_parceiro.SelectedIndex = 0;
            lista_status.SelectedIndex = 0;
            lista_mecanica.DataSource = "";
            lista_mecanica.DataBind();
            nome_usuario.Text = "";
            documento.Text = "";
        }

        protected void lista_parceiro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string parceiro = lista_parceiro.SelectedValue;

            if (lista_parceiro.SelectedIndex != 0)
            {
                Credito_Operações crd_banco = new Credito_Operações();

                var mecanica = crd_banco.ListaMecanica(Int32.Parse(parceiro));
                lista_mecanica.DataValueField = "id_mecanica";
                lista_mecanica.DataTextField = "desc_mecanica";
                lista_mecanica.DataSource = mecanica;
                lista_mecanica.DataBind();
            }
            else
            {
                lista_mecanica.DataSource = "";
                lista_mecanica.DataBind();
            }
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

    }
}