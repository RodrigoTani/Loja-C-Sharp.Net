using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models.Carrinho
{
    public class CarrinhodeComprasViewModel
    {
        [Key]
        public int RecordID { get; set; }
        public List<Carrinho> CartItems { get; set; }
        public decimal CartTotal { get; set; }
        public string FormaPagamento { get; set; }

        public IEnumerable<EnderecoEntrega> Endereco { get; set; }
    }
}