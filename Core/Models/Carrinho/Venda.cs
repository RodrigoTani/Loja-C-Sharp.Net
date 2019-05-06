using Loja.Models.Carrinho;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        
        public virtual ICollection<ItemVenda> ItemVendas { get; set; }
        public virtual ICollection<Pagamento> Formas { get; set; }


    }
}
