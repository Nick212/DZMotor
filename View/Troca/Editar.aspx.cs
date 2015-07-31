using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco;
using DzAnalyzer.Banco.Troca_DB;
using DzAnalyzer.Models.Cadastro;
using DzAnalyzer.Models.Troca;
using DzAnalyzer.Catalogo;

namespace DzAnalyzer.View
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        TrocaOperacoes to = new TrocaOperacoes();
        int tipoPessoaTela;
        protected void Page_Load(object sender, EventArgs e)
        {
            //litResposta.Text = "";
            //litAtualiza.Text = "";
            if (!IsPostBack)
            {
                //if (Request.QueryString["pedidoAnterior"] != null)
                //{
                //    litResposta.Text = "<script type='text/javascript' language='javascript'>  alert('Troca realizada com sucesso!\\nNúmero da troca: " + Request.QueryString["pedidoAnterior"] + "');  </script>";                     
                //}
                //if (Request.QueryString["atualizaTroca"] != null)
                //{
                //    litAtualiza.Text = "<script type='text/javascript' language='javascript'> alert('Troca atualizada com sucesso!'); </script>";
                //}
                
            }

        }        

        private void limpaCampos()
        {
            txbDocumento.Text = "";
            txbNomeClienteTroca.Text = "";
            txbRazaoSocial.Text = "";
            txbSaldoClienteTroca.Text = "";
            txbEmailClienteTroca.Text = "";
            txbEmail2ClienteTroca.Text = "";
            txbCNPJParceiro.Text = "";
            txbRazaoSocialParceiro.Text = "";
            txbNomeFantasiaParceiro.Text = "";
            txbDataTroca.Text = "";
            gvProdutosDisponiveis.DataSource = null;
            gvProdutosDisponiveis.DataBind();
            lblProdutosIndisponiveis.Visible = false;
        }


        protected void PessoaFisica_CheckedChanged1(object sender, EventArgs e)
        {
            txbSaldoClienteTroca.BackColor = System.Drawing.Color.White;
            limpaCampos();
            txbDocumento.Text = "";
            tipoPessoaTela = 1;
            lblDocumentoFisico.Visible = true;
            lbldocumentoJuridico.Visible = false;
            txbDocumento.Visible = true;
            btnPesquisarClienteTroca.Visible = true;
            lblNomeCliente.Visible = true;
            txbNomeClienteTroca.Visible = true;
            lblSaldoCliente.Visible = true;
            txbSaldoClienteTroca.Visible = true;
            lblEmailCliente.Visible = true;
            txbEmailClienteTroca.Visible = true;
            lblEmailReserva.Visible = true;
            txbEmail2ClienteTroca.Visible = true;
            lblRazaoSocial.Visible = false;
            txbRazaoSocial.Visible = false;

        }

        protected void PessoaJuridica_CheckedChanged(object sender, EventArgs e)
        {
            txbSaldoClienteTroca.BackColor = System.Drawing.Color.White;
            limpaCampos();
            txbDocumento.Text = "";
            tipoPessoaTela = 2;
            lbldocumentoJuridico.Visible = true;
            lblDocumentoFisico.Visible = false;
            txbDocumento.Visible = true;
            btnPesquisarClienteTroca.Visible = true;
            lblNomeCliente.Visible = false;
            txbNomeClienteTroca.Visible = false;
            lblRazaoSocial.Visible = true;
            txbRazaoSocial.Visible = true;
            lblSaldoCliente.Visible = true;
            txbSaldoClienteTroca.Visible = true;
            lblEmailCliente.Visible = true;
            txbEmailClienteTroca.Visible = true;
            lblEmailReserva.Visible = true;
            txbEmail2ClienteTroca.Visible = true;


        }

        protected void btnNovaTroca_Click(object sender, EventArgs e)
        {
            rbtnPessoaFisica.Visible = true;
            rbtnPessoaJuridica.Visible = true;
            lblCNPJParceiro.Visible = true;
            txbCNPJParceiro.Visible = true;
            lblRazaoSocialParceiro.Visible = true;
            txbRazaoSocialParceiro.Visible = true;
            lblNomeFantasiaParceiro.Visible = true;
            txbNomeFantasiaParceiro.Visible = true;
            txbNumTroca.Enabled = false;
            txbDataTroca.Text = DateTime.Now.ToString();
            txbDataTroca.Enabled = false;
            lblCliente.Visible = true;
            lblParceiro.Visible = true;
            lblProdutosDisponiveis.Visible = true;
            gvProdutosDisponiveis.Visible = true;
            btnCadastrarTroca.Visible = true;
            btnPesquisarTroca.Visible = false;
            btnCancelar.Visible = true;

        }

        protected void btnPesquisarClienteTroca_Click(object sender, EventArgs e)
        {
            txbSaldoClienteTroca.BackColor = System.Drawing.Color.White;
            CadastroUsuario cliente = new CadastroUsuario();
            if (rbtnPessoaFisica.Checked == true)
            {
                tipoPessoaTela = 1;
                cliente = to.PesquisaCliente(txbDocumento.Text, tipoPessoaTela);
                txbNomeClienteTroca.Text = cliente.nome;
                txbSaldoClienteTroca.Text = cliente.saldo.ToString();
                txbEmailClienteTroca.Text = cliente.email;
                txbEmail2ClienteTroca.Text = cliente.emailReserva;
                Session["idCliente"] = cliente.idUsuario;
                if (Convert.ToInt32(txbSaldoClienteTroca.Text) <= 0 && txbSaldoClienteTroca.Text != "")
                {
                    txbSaldoClienteTroca.BackColor = System.Drawing.Color.IndianRed;
                }
            }
            else if (rbtnPessoaJuridica.Checked == true)
            {
                tipoPessoaTela = 2;
                cliente = to.PesquisaCliente(txbDocumento.Text, tipoPessoaTela);
                txbRazaoSocial.Text = cliente.nome;
                txbSaldoClienteTroca.Text = cliente.saldo.ToString();
                txbEmailClienteTroca.Text = cliente.email;
                txbEmail2ClienteTroca.Text = cliente.emailReserva;
                Session["idCliente"] = cliente.idUsuario;
                if (Convert.ToInt32(txbSaldoClienteTroca.Text) <= 0 && txbSaldoClienteTroca.Text != "")
                {
                    txbSaldoClienteTroca.BackColor = System.Drawing.Color.IndianRed;
                }
            }
            lblProdutosDisponiveis.Visible = true;
            gvProdutosDisponiveis.DataSource = to.ListaDeProdutosPorSaldo(int.Parse(txbSaldoClienteTroca.Text));
            gvProdutosDisponiveis.DataBind();
            if (to.ListaDeProdutosPorSaldo(int.Parse(txbSaldoClienteTroca.Text)).Count <= 0)
            {
                lblProdutosIndisponiveis.Visible = true;
                lblProdutosDisponiveis.Visible = false;
            }
        }

        protected void gvProdutosDisponiveis_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelecionaParceiro")
            {
                
                int index = Convert.ToInt32(e.CommandArgument);
                TrocaFornecedor fornecedor = to.RetornaFornecedor(int.Parse(gvProdutosDisponiveis.Rows[index].Cells[0].Text));
                txbCNPJParceiro.Text = fornecedor.CNPJ;
                txbRazaoSocialParceiro.Text = fornecedor.razaoSocial;
                txbNomeFantasiaParceiro.Text = fornecedor.nomeFornecedor;
                Session["idFornecedor"] = fornecedor.idUsuario;
                Session["idProduto"] = int.Parse(gvProdutosDisponiveis.Rows[index].Cells[0].Text);
                gvProdutosDisponiveis.Rows[index].BackColor = System.Drawing.Color.LightGray;
            }
        }


        protected void btnCadastrarTroca_Click(object sender, EventArgs e)
        {
            Troca troca = new Troca();
            troca.Cliente = Convert.ToInt32(Session["idCliente"]);
            troca.Fornecedor = Convert.ToInt32(Session["idFornecedor"]);
            troca.Status = 11;
            troca.Produto = Convert.ToInt32(Session["idProduto"]);
            to.Insert(troca);
            to.AtualizaSaldo(to.PreçoProduto(troca.Produto), troca.Cliente);
            to.AtualizaEstoque(troca.Produto);
            txtAlerta.Text = "Troca realizada com sucesso!\nNúmero da troca: " + to.RetornaId();
            pModalAlerta.Visible = true;
            //Response.Redirect("Editar.aspx?pedidoAnterior="+to.RetornaId());           
            
        }

        protected void btnPesquisarTroca_Click(object sender, EventArgs e)
        {
            btnNovaTroca.Visible = false;
            txbDataTroca.Enabled = false;            
            Troca troca = to.Select(int.Parse(txbNumTroca.Text));
            CadastroUsuario cliente = to.SelectClienteTroca(troca.Cliente);
            TrocaFornecedor fornecedor = to.SelectFornecedorTroca(troca.Fornecedor);
            Produto produto = to.SelectProdutoTroca(troca.Produto);
            if (cliente.idTipoPessoa == 1)
            {
                rbtnPessoaJuridica.Visible = true;
                rbtnPessoaJuridica.Checked = false;
                rbtnPessoaFisica.Visible = true;
                rbtnPessoaFisica.Checked = true;
                lblDocumentoFisico.Visible = true;
                txbRazaoSocial.Visible = false;
                txbDocumento.Visible = true;
                txbDocumento.Text = cliente.documento;
                lblNomeCliente.Visible = true;
                txbNomeClienteTroca.Visible = true;
                txbNomeClienteTroca.Text = cliente.nome;
                lblSaldoCliente.Visible = true;
                txbSaldoClienteTroca.Visible = true;
                txbSaldoClienteTroca.Text = cliente.saldo.ToString();
                lblEmailCliente.Visible = true;
                txbEmailClienteTroca.Visible = true;
                txbEmailClienteTroca.Text = cliente.email;
                lblEmailReserva.Visible = true;
                txbEmail2ClienteTroca.Visible = true;
                txbEmail2ClienteTroca.Text = cliente.emailReserva;
            }
            else
            {
                rbtnPessoaFisica.Visible = true;
                rbtnPessoaFisica.Checked = false;
                rbtnPessoaJuridica.Visible = true;
                rbtnPessoaJuridica.Checked = true;
                lbldocumentoJuridico.Visible = true;
                txbDocumento.Visible = true;
                txbDocumento.Text = cliente.documento;
                lblRazaoSocial.Visible = true;
                txbRazaoSocial.Visible = true;
                txbRazaoSocial.Text = cliente.nome;
                lblSaldoCliente.Visible = true;
                txbSaldoClienteTroca.Visible = true;
                txbSaldoClienteTroca.Text = cliente.saldo.ToString();
                lblEmailCliente.Visible = true;
                txbEmailClienteTroca.Visible = true;
                txbEmailClienteTroca.Text = cliente.email;
                lblEmailReserva.Visible = true;
                txbEmail2ClienteTroca.Visible = true;
                txbEmail2ClienteTroca.Text = cliente.emailReserva;
            }
            List<Produto> produtoLista = new List<Produto>();
            produtoLista.Add(produto);
            gvEdicao.DataSource = produtoLista;
            gvEdicao.DataBind();
            gvEdicao.Visible = true;
            lblCNPJParceiro.Visible = true;
            txbCNPJParceiro.Visible = true;
            txbCNPJParceiro.Text = fornecedor.CNPJ;
            lblRazaoSocialParceiro.Visible = true;
            txbRazaoSocialParceiro.Visible = true;
            txbRazaoSocialParceiro.Text = fornecedor.razaoSocial;
            lblNomeFantasiaParceiro.Visible = true;
            txbNomeFantasiaParceiro.Visible = true;
            txbNomeFantasiaParceiro.Text = fornecedor.nomeFornecedor;
            btnAtualizarTroca.Visible = true;
            btnCancelar.Visible = true;

        }

        protected void gvEdicao_rowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProduto")
            {
                gvProdutosDisponiveis.Visible = true;
                gvEdicao.Visible = false;
                gvProdutosDisponiveis.DataSource = to.ListaDeProdutosPorSaldo(int.Parse(txbSaldoClienteTroca.Text));
                gvProdutosDisponiveis.DataBind();
                txbCNPJParceiro.Text = "";
                txbRazaoSocialParceiro.Text = "";
                txbNomeFantasiaParceiro.Text = "";
                if (to.ListaDeProdutosPorSaldo(int.Parse(txbSaldoClienteTroca.Text)).Count == 0)
                {
                    txbSaldoClienteTroca.BackColor = System.Drawing.Color.IndianRed;
                    lblProdutosIndisponiveis.Visible = true;
                    lblProdutosDisponiveis.Visible = false;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Editar.aspx");
        }

        protected void btnAtualizarTroca_Click(object sender, EventArgs e)
        {
            int produto = Convert.ToInt32(Session["idProduto"]);
            to.AtualizaTroca(produto, to.BuscarIdParceiro(produto), int.Parse(txbNumTroca.Text));
            txtAlerta.Text = "Troca atualizada com sucesso!";
            pModalAlerta.Visible = true;
            //Response.Redirect("Editar.aspx?atualizaTroca="+1);
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            pModalAlerta.Visible = false;
            Response.Redirect("Editar.aspx");
        }

       

    }
}