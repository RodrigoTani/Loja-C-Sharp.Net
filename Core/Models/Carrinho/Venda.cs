using Dominio;
using Loja.Models.Carrinho;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja.Models.Carrinho
{
    public class Venda
    {
        [Key]
        public int id { get; set; }

        private decimal total;

        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
        [ForeignKey("cupom_promocinal")]
        public int cupom_venda;
        
        public virtual ICollection<ItemVenda> ItemVendas { get; set; }
        public virtual ICollection<Pagamento> Formas { get; set; }
        public virtual Cupom cupom_promocinal { get; set; }

    }
}
