using Dominio;
using System;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Fornecedor : EntidadeDominio
    {
        
        [Required]
        public String Nome { get; set; }
        public enum TipoPessoa { Física, Jurídica }
        public String CNPJ { get; set; }
        //[Required]
        public String RazaoSocial { get; set; }

        public EnderecoEntrega Endereco { get; set; }


    }
}