using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Models.Carrinho
{
    public class CheckoutViewModel
    {
        public IEnumerable<CarrinhodeComprasViewModel> CarrinhodeComprasViewModel { get; set; }
        public EnderecoEntrega EnderecoEntrega { get; set; }

    }
}