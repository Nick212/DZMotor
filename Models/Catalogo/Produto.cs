using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Models.Parceiro;
using DzAnalyzer.Models.Catalogo;


namespace DzAnalyzer.Catalogo
{
    public class Produto
    {
        public int idProduto { get; set; }
        public Parceiro parceiro { get; set; }
        public SubCategoria subCategoria { get; set; }
        public ProdutoStatus status { get; set; }
        public string nomeProduto { get; set; }
        public int quantidadeProduto { get; set; }
        public string descricaoProduto { get; set; }
        public string marcaProduto { get; set; }
        public decimal valorProduto { get; set; }
        public int dotzProduto { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime? dataDesativacao { get; set; }

        public Produto()
        {
            parceiro = new Parceiro();
            subCategoria = new SubCategoria();
            status = new ProdutoStatus();
        }

       
    }
}