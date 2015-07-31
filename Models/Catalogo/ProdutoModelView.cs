using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Catalogo;

namespace DzAnalyzer.Models.Catalogo
{
    public class ProdutoModelView
    {
        public int IdProduto { get; set; }
        public string NomeFantasia { get; set; }
        public string DescricaoCategoria { get; set; }
        public string DescricaoSubCategoria { get; set; }
        public string DescricaoProdutoStatus { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidateProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public string MarcaProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public int DotzProduto { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime? dataDesativacao { get; set; }
        public ProdutoStatus status { get; set; }


        public ProdutoModelView()
        {
            status = new ProdutoStatus();
        }

        /// <summary>
        /// Metodo para retornar os campos nessesarios para o GridVid, porque a grid não aceita calsses com composição
        /// </summary>
        /// <param name="produtos">Uma lista de objetos produtos compostos</param>
        /// <returns>Retorna uma lista de um modelo do objeto produto</returns>
        public List<ProdutoModelView> CriarListaProdutoModalView(List<Produto> produtos)
        {

            List<ProdutoModelView> produtosModalView = new List<ProdutoModelView>();

            foreach (var produto in produtos)
            {
                ProdutoModelView produtoModalView = new ProdutoModelView();

                produtoModalView.IdProduto = produto.idProduto;
                produtoModalView.NomeFantasia = produto.parceiro.nome_fantasia;
                produtoModalView.DescricaoCategoria = produto.subCategoria.categoria.descricaoCategoria;
                produtoModalView.DescricaoSubCategoria = produto.subCategoria.descricaoSubCategoria;
                produtoModalView.DescricaoProdutoStatus = produto.status.descricaoStatus;
                produtoModalView.NomeProduto = produto.nomeProduto;
                produtoModalView.QuantidateProduto = produto.quantidadeProduto;
                produtoModalView.DescricaoProduto = produto.descricaoProduto;
                produtoModalView.MarcaProduto = produto.marcaProduto;
                produtoModalView.ValorProduto = produto.valorProduto;
                produtoModalView.DotzProduto = produto.dotzProduto;
                produtoModalView.dataCriacao = produto.dataCriacao;
                produtoModalView.dataDesativacao = produto.dataDesativacao;
                produtoModalView.status.idStatus = produto.status.idStatus;

                produtosModalView.Add(produtoModalView);

            }

            return produtosModalView;

        }

    }
}