using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loja.Models
{
    public class Cartao : EntidadeDominio
    {
        public string Usuario { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public DateTime DataExpira { get; set; }
        public string CVV  { get; set; }
    }
}