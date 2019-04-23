using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cupom : EntidadeDominio
    {
        public String Codigo { get; set; }
        public decimal Valor  { get; set; }
        public bool Ativo { get; set; }

        public Cupom()
        {
            Codigo = string.Empty;
            Valor = new Decimal();
            Ativo = new Boolean();
        }
    }
}
