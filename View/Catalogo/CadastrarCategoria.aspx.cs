
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.CatalogoBD;
using DzAnalyzer.Models.Catalogo;
namespace DzAnalyzer.View.Catalogo
{
    public partial class EditarProduto : System.Web.UI.Page
    {

        private CategoriaOperações categoriaBanco = new CategoriaOperações();
        /// <summary>
        /// Variavel para colocar todas as mensagens no modal;
        /// </summary>
        private string alerta = "";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region Evento
        /// <summary>
        /// Evento para abrir os campos da categoria 
        /// </summary>
        protected void btnCadastarCategoria_Click(object sender, EventArgs e)
        {
            PanelSubCategoria.Visible = false;
            PanelCategoria.Visible = true;
            PanelRodape.Visible = true;
        }

        /// <summary>
        /// Evento para abrir os campos da subCategoria e carregar o dropdownlist da categoria
        /// </summary>
        protected void btnCadastrarSubCategoria_Click(object sender, EventArgs e)
        {
            var categorias = categoriaBanco.ListaCategoriaBanco();
            ddlCategoria.DataValueField = "idCategoria";
            ddlCategoria.DataTextField = "descricaoCategoria";
            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Selecione uma Categoria"));

            PanelCategoria.Visible = false;
            PanelSubCategoria.Visible = true;
            PanelRodape.Visible = true;
        }

        /// <summary>
        /// Evento para Cadastrar uma Categoria ou SubCategoria
        /// Caso der erro abre um modal com a informaçao do erro
        /// Se der certo vai chamar um metodo do banco para inserir e abrir o modal informando o sucesso
        /// </summary>
        protected void btnCadastrarCategoria_Click(object sender, EventArgs e)
        {
            var categoria = new Categoria();
            var subCategoria = new SubCategoria(categoria);

            if (PanelCategoria.Visible)
            {

                VerificaCampoVazio(txtCategoria.Text, "Categoria");
                VerificaCampoVazio(txtCategoriaSubCategoria.Text, "SubCategoria");

                if (alerta == "")
                {

                    categoria.descricaoCategoria = txtCategoria.Text;

                    subCategoria.descricaoSubCategoria = txtCategoriaSubCategoria.Text;

                    int retorno = categoriaBanco.CadastrarCategoria(subCategoria);

                    if (retorno == 0)
                    {
                        alerta = "Sucesso!!";

                        ModalAlerta.Visible = true;
                        txtAlerta.Text = alerta;
                    }
                    else
                    {
                        alerta = "Não foi possivel cadastrar essa Categoria!";

                        ModalAlerta.Visible = true;
                        txtAlerta.Text = alerta;
                    }
                }
                else
                {
                    ModalAlerta.Visible = true;
                    txtAlerta.Text = alerta;
                }
            }
            else
            {
                VerificaCategoriaDDL();
                VerificaCampoVazio(txtSubCategoriaSubCategoria.Text, "SubCategoria");

                if (alerta == "")
                {

                    categoria.idCategoria = Int32.Parse(ddlCategoria.SelectedValue);

                    if (categoria.idCategoria == 0)
                    {
                        alerta = "Você deve selecionar uma Categoria!";

                        ModalAlerta.Visible = true;
                        txtAlerta.Text = alerta;
                        return;
                    }

                    subCategoria.descricaoSubCategoria = txtSubCategoriaSubCategoria.Text;

                    int retorno = categoriaBanco.CadastrarSubCategoria(subCategoria);

                    if (retorno == 0)
                    {
                        ModalAlerta.Visible = true;
                        txtAlerta.Text = "Sucesso!!";
                    }
                    else
                    {
                        ModalAlerta.Visible = true;
                        txtAlerta.Text = "Não foi possivel cadastrar essa SubCategoria!";
                    }
                }
                else
                {
                    ModalAlerta.Visible = true;
                    txtAlerta.Text = alerta;
                }

            }

            LimparCampos();

            PanelCategoria.Visible = false;
            PanelSubCategoria.Visible = false;
            PanelRodape.Visible = false;

        }

        /// <summary>
        /// Evento para fechar o modal
        /// </summary>
        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

        #endregion

        #region Metodo
        /// <summary>
        /// Limpar os campos da tela.
        /// </summary>
        private void LimparCampos()
        {
            txtCategoria.Text = "";
            txtCategoriaSubCategoria.Text = "";
            txtSubCategoriaSubCategoria.Text = "";
        }

        /// <summary>
        /// Verifica se um campo da tela está em branco e adiciona na variavel alerta o nome do campo que está em branco
        /// </summary>
        /// <param name="valor"> variavel do campo da tela</param>
        /// <param name="campo"> nome do campo da tela para adiconar no alerta</param>
        private void VerificaCampoVazio(string valor, string campo)
        {
            if (valor == "")
            {
                alerta = alerta + "O campo " + campo + " é obrigatório!\n";
            }
        }

        /// <summary>
        /// Verifica o campo dropdownlist se foi preenchido, caso não foi adiciona o erro na variavel alerta
        /// </summary>
        private void VerificaCategoriaDDL()
        {
            if (ddlCategoria.SelectedIndex == 0 || ddlCategoria.SelectedValue == "")
            {
                alerta = alerta + "O campo Categoria é obrigatório!!\n";
            }
        }


        #endregion

    }
}