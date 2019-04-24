using Dominio;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Web.Mvc;

namespace Loja.Models
{
    public class EnderecoEntrega : EntidadeDominio
    {
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string CEP { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public string Numero { get; set; }

        public string Observacao { get; set; }


    }
}