using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Models.Carrinho
{
    public class DetalhesPedido
    {
        public int DetalhesPedidoId { get; set; }

        public int PedidoId { get; set; }

        public int ProdutoId { get; set; }

        public int Quantidade { get; set; }

        public decimal? PrecoUnitario { get; set; }
    }
}