using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class EntidadeDominio
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime DataCadastro { get; set; }
        //public string StrBusca { get; set; }
    }
}
