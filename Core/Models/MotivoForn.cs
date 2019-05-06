using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Loja.Models
{
    public class MotivoForn 
    {
        public int id { get; set; }
        public DateTime DataMotivo { get; set; }

        [ForeignKey("forn")]
        public int fornecedo { get; set; }
        public virtual Fornecedor forn { get; set; }

        public String Usuario { get; set; }
        public String MotivoAtivacao { get; set; }
        public String MotivoInativacao { get; set; }
    }
}