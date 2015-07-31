using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DzAnalyzer.Models.Catalogo
{
    public class SubCategoria
    {
        public int idSubCategoria { get; set; }
        public string descricaoSubCategoria { get; set; }
        public Categoria categoria { get; set; }

        public SubCategoria(Categoria categoria)
        {
            this.categoria = categoria;
        }

        public SubCategoria()
        {
            categoria = new Categoria();
        }
    }
}
