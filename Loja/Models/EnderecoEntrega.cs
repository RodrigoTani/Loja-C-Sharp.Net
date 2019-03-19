using Dominio;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class EnderecoEntrega : EntidadeDominio
    {
        public string Usuario { get; set; }
        //[Required]
        public string CEP { get; set; }
        //[Required(ErrorMessage = "Campo Obrigatório")]
        public string Estado { get; set; }
        //[Required]
        public string Cidade { get; set; }
        //[Required]
        public string Bairro { get; set; }
        [Required]
        public string Logradouro { get; set; }
        //[Required]
        public string Numero { get; set; }

        public string Observacao { get; set; }

    }
}