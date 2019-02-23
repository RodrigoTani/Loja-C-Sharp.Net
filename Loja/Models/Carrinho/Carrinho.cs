using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Loja.Models.Carrinho
{
    public class Carrinho
    {
        [Key]
        public int RecordId { get; set; }
        public string CarrinhoId { get; set; }

        [Required(AllowEmptyStrings = true, ErrorMessage = " ")]
        [Range(0, 100, ErrorMessage = "Quantidade precisa estar entre 0 e 100")]
        public int Quantidade { get; set; }

        public System.DateTime DataCriacao { get; set; }

        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }
        public string FormaPagamento { get; set; }
    }
}