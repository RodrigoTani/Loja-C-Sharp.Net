using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loja.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Models.Carrinho
{
    public class Pagamento
    {
        [Key]
        public int id { get; set; }
        private decimal valor;

        public decimal Valor 
        {
            get { return valor; }
            set { valor = value; }
        }

        public int Cartaoid { get; set; }
        [ForeignKey("Cartaoid")]
        public virtual Cartao Cartao { get; set; }

        public virtual Venda Venda { get; set; }


    }
}
