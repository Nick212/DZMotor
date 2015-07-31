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
    public partial class CadastrarProduto : System.Web.UI.Page
    {
        private CategoriaOperações cetegoriasOperacoes = new CategoriaOperações();
        private ParceiroOperacoes parceirosOperacoes = new ParceiroOperacoes();
        private StatusProdutoOperações statusOperacoes = new StatusProdutoOperações();
        private ProdutoOperações produtosOperacoes = new ProdutoOperações();
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
                ddlFornecedor.Items.Insert(0, new ListItem("Selecione um Fornecedor"));

                var status = statusOperacoes.ListaStatus();
                var statusProdutos = status.Where(x => x.idStatus == (int)eStatusProduto.Ativo || x.idStatus == (int)eStatusProduto.Indisponível);
                ddlStatus.DataValueField = "idStatus";
                ddlStatus.DataTextField = "descricaoStatus";
                ddlStatus.DataSource = statusProdutos;
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("Selecione um Status"));
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
        }

        /// <summary>
        /// Chama as validações para verificar os campos, se tudo estiver correto cadastra o produto se não mostra o modal com a menssagem de erro.
        /// </summary>
        protected void btnCadastarProduto_Click(object sender, EventArgs e)
        {

            VerificarCampos();

            if (alerta != "")
            {
                txtAlerta.Text = alerta;
                ModalAlerta.Visible = true;
            }
            else
            {
                if (produtosOperacoes.CadastrarProduto(DadosProduto()) == 0)
                {
                    LimparCampos();
                    txtAlerta.Text = "Sucesso!";
                    ModalAlerta.Visible = true;
                    return;
                }
                if (produtosOperacoes.CadastrarProduto(DadosProduto()) == 2627)
                {
                    LimparCampos();
                    txtAlerta.Text = "(Erro)Esse Produto já foi Cadastrado!";
                    ModalAlerta.Visible = true;
                    return;
                }
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
                txtdz.Text = resultado.ToString();
            }
            else
            {
                txtAlerta.Text = "O campo Valor é obrigatório!\n";
                ModalAlerta.Visible = true;
            }

        }

        /// <summary>
        /// Fecha o Modal quando o botão é clicado
        /// </summary>   
        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Cria o objeto produto com base nas informações recebidas da tela.
        /// </summary>
        /// <returns>Retorna um objeto do tipo Produto</returns>
        private Produto DadosProduto()
        {
            var produto = new Produto();

            produto.parceiro.id_parceiro = Int32.Parse(ddlFornecedor.SelectedValue);
            produto.subCategoria.idSubCategoria = Int32.Parse(ddlSubCategoria.SelectedValue);
            produto.status.idStatus = Int32.Parse(ddlStatus.SelectedValue);
            produto.nomeProduto = txtNome.Text;
            produto.quantidadeProduto = Int32.Parse(txtQuantidade.Text);
            produto.descricaoProduto = txtDesc.Text;
            produto.marcaProduto = txtMarca.Text;
            produto.valorProduto = Decimal.Parse(txtValor.Text + "," + txtCentavos.Text);
            produto.dotzProduto = Int32.Parse(txtdz.Text);

            return produto;
        }

        /// <summary>
        /// Chama todos os metodos de verificações de campos da tela.
        /// </summary>
        private void VerificarCampos()
        {
            VerificaParceiro();
            VerificaCategoria();
            VerificaSubCategoria();
            VerificaStatus();
            VerificaCampoVazio(txtNome.Text, "Nome");
            VerificaCampoVazio(txtMarca.Text, "Marca");
            VerificaCampoVazio(txtValor.Text, "Valor em R$");
            VerificaCampoVazio(txtCentavos.Text, "Centavos");
            VerificaCampoVazio(txtdz.Text, "Valor em DZ");
            VerificaCampoVazio(txtQuantidade.Text, "Quantidade");
            VerificaCampoVazio(txtDesc.Text, "Descrição");
        }

        /// <summary>
        /// Verifica o dropDownList se foi selecionado algum campo, caso não adiciona na variavel alerta o valor do erro.
        /// </summary>
        private void VerificaParceiro()
        {
            if (ddlFornecedor.SelectedIndex == 0)
            {
                alerta = alerta + "Verifique o campo Fornecedor!\n";

            }
        }

        /// <summary>
        /// Verifica o dropDownList se foi selecionado algum campo, caso não adiciona na variavel alerta o valor do erro.
        /// </summary>
        private void VerificaCategoria()
        {
            if (ddlCategoria.SelectedIndex == 0)
            {
                alerta = alerta + "Verifique o campo Categoria!\n";
            }
        }

        /// <summary>
        /// Verifica o dropDownList se foi selecionado algum campo, caso não adiciona na variavel alerta o valor do erro.
        /// </summary>
        private void VerificaSubCategoria()
        {
            if (ddlSubCategoria.SelectedIndex == 0 || ddlSubCategoria.SelectedValue == "")
            {
                alerta = alerta + "Verifique o campo SubCategoria!\n";
            }
        }

        /// <summary>
        /// Verifica o dropDownList se foi selecionado algum campo e aplica a regra para ele, caso caia em alguma regra adiciona na variavel alerta o valor do erro.
        /// </summary>
        private void VerificaStatus()
        {
            if (ddlStatus.SelectedIndex == 0)
            {
                alerta = alerta + "Verifique o campo Status!\n";
            }
            if (txtQuantidade.Text == "")
            {
                return;
            }
            if (ddlStatus.SelectedIndex == (int)(eStatusProduto.Indisponível - 1) && Int32.Parse(txtQuantidade.Text) > 0)
            {
                alerta = alerta + "O Produto está no status Indisponível a quantidade tem que ser 0\n";
            }
            if (ddlStatus.SelectedIndex == (int)eStatusProduto.Ativo && Int32.Parse(txtQuantidade.Text) <= 0)
            {
                alerta = alerta + "O Produto está no status Ativo a quantidade tem que ser maior que 0\n";
            }
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
        /// Limpa os campos da tela
        /// </summary>
        private void LimparCampos()
        {
            ddlFornecedor.SelectedIndex = 0;
            ddlCategoria.SelectedIndex = 0;
            ddlSubCategoria.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            txtNome.Text = "";
            txtMarca.Text = "";
            txtValor.Text = "";
            txtCentavos.Text = "";
            txtdz.Text = "";
            txtQuantidade.Text = "";
            txtDesc.Text = "";

        }
        #endregion

    }
}