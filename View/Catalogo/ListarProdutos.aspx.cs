using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco.CatalogoBD;
using DzAnalyzer.Banco.ParceiroBD;
using DzAnalyzer.Catalogo;
using DzAnalyzer.Models.Parceiro;
using DzAnalyzer.Models.Catalogo;


namespace DzAnalyzer.View.Catalogo
{
    public partial class ListarProdutos : System.Web.UI.Page
    {
        private CategoriaOperações cetegoriasOperacoes = new CategoriaOperações();
        private ParceiroOperacoes parceirosOperacoes = new ParceiroOperacoes();
        private StatusProdutoOperações statusOperacoes = new StatusProdutoOperações();
        private ProdutoOperações produtosOperacoes = new ProdutoOperações();
        List<ProdutoModelView> produtos = new List<ProdutoModelView>();
        /// <summary>
        /// Variavel para identificar a pagina da GridView
        /// </summary>
        private int pagina = 0;
        /// <summary>
        /// Variavel para colocar todas as mensagens no modal;
        /// </summary>
        private string alerta = "";

        /// <summary>
        /// Carrega os dropDownList das categorias, parceiros e status
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var categorias = cetegoriasOperacoes.ListaCategoriaBanco();
                ddlCategoria.DataValueField = "idCategoria";
                ddlCategoria.DataTextField = "descricaoCategoria";
                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("Selecione uma Categoria"));

                var parceiros = parceirosOperacoes.ListarParceiros();
                var parceirosTipo = parceiros.Where(x => x.tipo_parceiro == (int)eTipoParceiro.troca || x.tipo_parceiro == (int)eTipoParceiro.creditotroca);

                ddlFornecedor.DataValueField = "id_parceiro";
                ddlFornecedor.DataTextField = "nome_fantasia";
                ddlFornecedor.DataSource = parceirosTipo;
                ddlFornecedor.DataBind();
                ddlFornecedor.Items.Insert(0, new ListItem("Selecione um Parceiro"));

                var status = statusOperacoes.ListaStatus();
                ddlStatus.DataValueField = "idStatus";
                ddlStatus.DataTextField = "descricaoStatus";
                ddlStatus.DataSource = status;
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("Selecione uma Categoria"));
            }
        }

        #region Eventos

        /// <summary>
        /// Carrega o dropDownList da subCategoria quando a Categoria é selecionada
        /// </summary>
        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selecionado = ddlCategoria.SelectedValue;

            if (ddlCategoria.SelectedIndex != 0)
            {
                var subCategorias = cetegoriasOperacoes.ListaSubCategoriaBanco(selecionado);
                ddlSubCategoria.DataValueField = "idSubCategoria";
                ddlSubCategoria.DataTextField = "descricaoSubCategoria";
                ddlSubCategoria.DataSource = subCategorias;
                ddlSubCategoria.DataBind();
                ddlSubCategoria.Items.Insert(0, new ListItem("Selecione uma SubCategoria"));
            }
            else
            {
                ddlSubCategoria.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Monta o GridView
        /// </summary>
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            MontarGrid(ListaProdutos(), pagina);
        }

        /// <summary>
        /// Monta a GridView e verifica e pega a pagina que está na GridView
        /// </summary>
        protected void gvProdutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            pagina = e.NewPageIndex;
            MontarGrid(produtos, pagina);
        }

        /// <summary>
        /// Quando o botão do GridView é selecionado monta o modal para editar passando os dados da linha selecionada.
        /// </summary>
        protected void gvProdutos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            ProdutoModelView produto = new ProdutoModelView();
            produto.IdProduto = Int32.Parse(e.CommandArgument.ToString());

            List<ProdutoModelView> produtos = ListaProdutos();

            foreach (var item in produtos)
            {
                if (item.IdProduto == produto.IdProduto)
                {
                    produto = item;

                    string[] valorEmReais = produto.ValorProduto.ToString().Split(new Char[] { ',' });
                    string valorReais = valorEmReais[0];
                    string valorCentavos = valorEmReais[1].Substring(0, 2);

                    LabelNome.Text = produto.NomeProduto;
                    txtMarca.Text = produto.MarcaProduto;

                    var status = statusOperacoes.ListaStatus();
                    ddlStatusModal.DataValueField = "idStatus";
                    ddlStatusModal.DataTextField = "descricaoStatus";
                    ddlStatusModal.DataSource = status;
                    ddlStatusModal.DataBind();

                    ddlStatusModal.SelectedIndex = produto.status.idStatus - 1;
                    txtDz.Text = produto.DotzProduto.ToString();
                    txtValor.Text = valorReais;
                    txtCentavos.Text = valorCentavos;
                    txtQuantidade.Text = produto.QuantidateProduto.ToString();
                    txtDesc.Text = produto.DescricaoProduto;
                    LabelID.Text = produto.IdProduto.ToString();

                    Modal.Visible = true;
                    return;
                }
            }
        }

        /// <summary>
        /// Fecha o Modal da GridView
        /// </summary>
        protected void btn_fechar_Click(object sender, EventArgs e)
        {
            Modal.Visible = false;
        }

        /// <summary>
        /// Fecha o modal do alerta
        /// </summary>
        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

        /// <summary>
        /// Salva as alterações no banco, verifica se nao tem nenhum campo null e fecha o modal.
        /// </summary>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            VerificarCampos();

            if (alerta == "")
            {
                Produto produto = new Produto();
                produto.marcaProduto = txtMarca.Text;
                produto.status.idStatus = Int32.Parse(ddlStatusModal.SelectedValue);
                produto.valorProduto = Decimal.Parse(txtValor.Text + "," + txtCentavos.Text);
                produto.dotzProduto = Int32.Parse(txtDz.Text);
                produto.quantidadeProduto = Int32.Parse(txtQuantidade.Text);
                produto.descricaoProduto = txtDesc.Text;
                produto.idProduto = Int32.Parse(LabelID.Text);
                produto.dataDesativacao = null;

                if (Int32.Parse(txtValor.Text) * 11 != Int32.Parse(txtDz.Text))
                {
                    ModalAlerta.Visible = true;
                    txtAlerta.Text = "Você deve Calcular o valor em Dotz!";
                    return;
                }

                int retorno;

                if (produto.status.idStatus == 2)
                {
                    retorno = produtosOperacoes.AtualizarProdutoDataDesativacao(produto);
                }
                else
                {
                    retorno = produtosOperacoes.AtualizarProduto(produto);
                }

                if (retorno == 0)
                {
                    ModalAlerta.Visible = true;
                    Modal.Visible = false;
                    txtAlerta.Text = "Sucesso!!";

                    MontarGrid(ListaProdutos(), pagina);
                }
                else
                {
                    ModalAlerta.Visible = true;
                    txtAlerta.Text = "Não foi possivel fazer a sua atualização!";
                }
            }
            else
            {
                ModalAlerta.Visible = true;
                txtAlerta.Text = alerta;
            }

        }

        /// <summary>
        /// Calcula o calor em dotz baseado no valor digitado no campo txtValor e adiciona o calculo no campo txtdz
        /// </summary>
        protected void CalcularDz_Click(object sender, EventArgs e)
        {
            if ((txtValor.Text != "") && (txtCentavos.Text != ""))
            {
                int valor = Int32.Parse(txtValor.Text);
                int resultado = valor * 11;
                txtDz.Text = resultado.ToString();
            }
            else
            {
                txtAlerta.Text = "O campo Valor é obrigatório!\n";
                ModalAlerta.Visible = true;
            }

        }

        #endregion

        #region Método

        /// <summary>
        /// Verifica os campos selecionados na tela e faz a consulta no banco de acordo com os dados selecionados
        /// </summary>
        /// <returns>Uma lista do objeto ProdutoModalView</returns>
        private List<ProdutoModelView> ListaProdutos()
        {
            if (ddlFornecedor.SelectedIndex == 0 && ddlStatus.SelectedIndex == 0 && ddlCategoria.SelectedIndex == 0 && (ddlSubCategoria.SelectedIndex == 0 || ddlSubCategoria.SelectedValue == ""))
            {
                produtos = produtosOperacoes.ListaProdutoBanco();
                return produtos;
            }
            // Parceiro
            if (ddlFornecedor.SelectedIndex != 0 &&
                ddlStatus.SelectedIndex == 0 &&
                ddlCategoria.SelectedIndex == 0 &&
                ddlSubCategoria.SelectedIndex <= 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoParceiro(ddlFornecedor.SelectedValue);
                return produtos;
            }
            //SubCategoria
            if (ddlFornecedor.SelectedIndex == 0 &&
                ddlStatus.SelectedIndex == 0 &&
                ddlCategoria.SelectedIndex != 0 &&
                ddlSubCategoria.SelectedIndex > 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoSubCategoria(ddlSubCategoria.SelectedValue);
                return produtos;
            }
            //Status
            if (ddlFornecedor.SelectedIndex == 0 &&
               ddlStatus.SelectedIndex != 0 &&
               ddlCategoria.SelectedIndex == 0 &&
               ddlSubCategoria.SelectedIndex <= 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoStatus(ddlStatus.SelectedValue);
                return produtos;
            }
            //Parceiro Status
            if (ddlFornecedor.SelectedIndex != 0 &&
               ddlStatus.SelectedIndex != 0 &&
               ddlCategoria.SelectedIndex == 0 &&
               ddlSubCategoria.SelectedIndex <= 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoParceiroStatus(ddlFornecedor.SelectedValue, ddlStatus.SelectedValue);
                return produtos;
            }
            //Parceiro SubCategoria
            if (ddlFornecedor.SelectedIndex != 0 &&
               ddlStatus.SelectedIndex == 0 &&
               ddlCategoria.SelectedIndex != 0 &&
               ddlSubCategoria.SelectedIndex > 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoParceiroSubCategoria(ddlFornecedor.SelectedValue, ddlSubCategoria.SelectedValue);
                return produtos;
            }
            //SubCategoria Status
            if (ddlFornecedor.SelectedIndex == 0 &&
              ddlStatus.SelectedIndex != 0 &&
              ddlCategoria.SelectedIndex != 0 &&
              ddlSubCategoria.SelectedIndex > 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoSubCategoriaStatus(ddlSubCategoria.SelectedValue, ddlStatus.SelectedValue);
                return produtos;
            }
            // Parceiro SubCategoria Status
            if (ddlFornecedor.SelectedIndex != 0 &&
               ddlStatus.SelectedIndex != 0 &&
               ddlCategoria.SelectedIndex != 0 &&
               ddlSubCategoria.SelectedIndex > 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoParceiroSubCategoriaStatus(ddlFornecedor.SelectedValue, ddlSubCategoria.SelectedValue, ddlStatus.SelectedValue);
                return produtos;
            }
            
            //Categoria
            if (ddlFornecedor.SelectedIndex == 0 &&
               ddlStatus.SelectedIndex == 0 &&
               ddlCategoria.SelectedIndex != 0 &&
               ddlSubCategoria.SelectedIndex == 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoCategoria(ddlCategoria.SelectedValue);
                return produtos;
            }

            //Categoria Parceiro
            if (ddlFornecedor.SelectedIndex != 0 &&
               ddlStatus.SelectedIndex == 0 &&
               ddlCategoria.SelectedIndex != 0 &&
               ddlSubCategoria.SelectedIndex == 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoCategoriaParceiro(ddlCategoria.SelectedValue,ddlFornecedor.SelectedValue);
                return produtos;
            }
            //Categoria Status
            if (ddlFornecedor.SelectedIndex == 0 &&
               ddlStatus.SelectedIndex != 0 &&
               ddlCategoria.SelectedIndex != 0 &&
               ddlSubCategoria.SelectedIndex == 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoCategoriaStatus(ddlCategoria.SelectedValue, ddlStatus.SelectedValue);
                return produtos;
            }

            //Categoria Status Parceiro
            if (ddlFornecedor.SelectedIndex != 0 &&
               ddlStatus.SelectedIndex != 0 &&
               ddlCategoria.SelectedIndex != 0 &&
               ddlSubCategoria.SelectedIndex == 0)
            {
                produtos = produtosOperacoes.ListaProdutoBancoCategoriaParceiroStatus(ddlCategoria.SelectedValue, ddlFornecedor.SelectedValue, ddlStatus.SelectedValue);
                return produtos;
            }
            return produtos;
        }

        /// <summary>
        /// Monta o GridView e exibe na tela
        /// </summary>
        /// <param name="produtos">Uma lista do objeto ProdutoModalView para colocar no dataSurce da gridview</param>
        /// <param name="pagina">o numero da pagina que vai iniciar o gridview</param>
        private void MontarGrid(List<ProdutoModelView> produtos, int pagina)
        {
            if (produtos.Count != 0)
            {
                gvProdutos.PageIndex = pagina;
                gvProdutos.DataSource = produtos;
                gvProdutos.DataBind();
                gvProdutos.HeaderRow.Cells[0].Text = "EDITAR";
                gvProdutos.HeaderRow.Cells[1].Text = "ID PRODUTO";
                gvProdutos.HeaderRow.Cells[2].Text = "NOME PARCEIRO";
                gvProdutos.HeaderRow.Cells[3].Text = "CATEGORIA";
                gvProdutos.HeaderRow.Cells[4].Text = "SUB CATEGORIA";
                gvProdutos.HeaderRow.Cells[5].Text = "STATUS";
                gvProdutos.HeaderRow.Cells[6].Text = "NOME PRODUTO";
                gvProdutos.HeaderRow.Cells[7].Text = "QUANTIDADE";
                gvProdutos.HeaderRow.Cells[8].Text = "DESCARIÇÃO PRODUTO";
                gvProdutos.HeaderRow.Cells[9].Text = "MARCA";
                gvProdutos.HeaderRow.Cells[10].Text = "VALOR(R$)";
                gvProdutos.HeaderRow.Cells[11].Text = "DOTZ";
                gvProdutos.HeaderRow.Cells[12].Text = "DATA CRIAÇÃO";
                gvProdutos.HeaderRow.Cells[13].Text = "DATA DESATIVAÇÃO";
            }
            else
            {
                gvProdutos.DataSource = produtos;
                gvProdutos.DataBind();
                
                alerta = "Não existe produtos com essas especificações !";
                ModalAlerta.Visible = true;
                txtAlerta.Text = alerta;

                LimparCampos();

            }
        }

        /// <summary>
        /// Limpa os campos da tela
        /// </summary>
        private void LimparCampos()
        {
            ddlCategoria.SelectedIndex = 0;
            ddlSubCategoria.SelectedIndex = 0;
            ddlFornecedor.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }

        /// <summary>
        /// Chama todos os metodos de verificações de campos da tela.
        /// </summary>
        private void VerificarCampos()
        {
            VerificaCampoVazio(txtMarca.Text, "Marca");
            VerificaCampoVazio(txtValor.Text, "Valor em R$");
            VerificaCampoVazio(txtCentavos.Text, "Centavos");
            VerificaCampoVazio(txtDz.Text, "Valor em DZ");
            VerificaCampoVazio(txtQuantidade.Text, "Quantidade");
            VerificaCampoVazio(txtDesc.Text, "Descrição");
            VerificaStatus();
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
        /// Verifica o dropDownList se foi selecionado algum campo e aplica a regra para ele, caso caia em alguma regra adiciona na variavel alerta o valor do erro.
        /// </summary>
        private void VerificaStatus()
        {
            if (txtQuantidade.Text == "")
            {
                return;
            }
            if (ddlStatusModal.SelectedIndex == (int)eStatusProduto.Indisponível -1 && Int32.Parse(txtQuantidade.Text) > 0)
            {
                alerta = alerta + "O Produto está no status Indisponível a quantidade tem que ser 0\n";
            }
            if (ddlStatusModal.SelectedIndex == (int)eStatusProduto.Ativo -1 && Int32.Parse(txtQuantidade.Text) <= 0)
            {
                alerta = alerta + "O Produto está no status Ativo a quantidade tem que ser maior que 0\n";
            }
        }

        #endregion



    }
}

