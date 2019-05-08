using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loja.Models.Carrinho
{
    public class PedidoStatus
    {
        public int id { get; set; }

        [ForeignKey("pedi")]
        public int PedidoId { get; set; }

        [ForeignKey("statu")]
        public int StatusId { get; set; }
        public string Usuario { get; set; }
        public DateTime DataStatus { get; set; }
        public String Motivo { get; set; }


        //Auxiliares
        public virtual Pedido pedi { get; set; }
        public virtual Status statu { get; set; }

    }
}