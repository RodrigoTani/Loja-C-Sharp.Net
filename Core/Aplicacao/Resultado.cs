using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aplicacao
{
    public class Resultado
    {
        public string Mensagem { get; set; }
        public List<EntidadeDominio> Entidades { get; set; }

        public Resultado()
        {
            Entidades = new List<EntidadeDominio>();
            Mensagem = string.Empty;
        }
    }
}
