using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Models.Carrinho
{
    [Bind(Exclude = "PedidoId")]
    public class Pedido
    {
        [ScaffoldColumn(false)]
        public int PedidoId { get; set; }
        [ScaffoldColumn(false)]
        public string Usuario { get; set; }
        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        [ScaffoldColumn(false)]
        public System.DateTime DataPedido { get; set; }
        public List<DetalhesPedido> DetalhesPedidos { get; set; }

        public string FormaPagamento { get; set; }
    }
}