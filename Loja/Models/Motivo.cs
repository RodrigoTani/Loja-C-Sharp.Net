using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loja.Models
{
    public class Motivo
    {
        public int id { get; set; }
        public DateTime DataMotivo { get; set; }

        [ForeignKey("prodid")]
        public int ProdutoId { get; set; }
        public virtual Produto prodid { get; set; }

        public String Usuario { get; set; }
        public String MotivoAtivacao { get; set; }
        public String MotivoInativacao { get; set; }
    }
}