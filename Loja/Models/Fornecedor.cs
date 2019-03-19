using Dominio;
using System;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Fornecedor : EntidadeDominio
    {
        
        [Required]
        public String RazaoSocial { get; set; }
        public TipoPessoa tipoPessoa { get; set; }
        public String CNPJ { get; set; }
        //[Required]
        public String NomeVendedor  { get; set; }
        public String CPF { get; set; }
        public String Observacao { get; set; }
        public String Email { get; set; }
        public EnderecoEntrega EnderecoEntrega { get; set; }
        public bool Ativo { get; set; } = true;

        //Auxiliares

       
        public enum TipoPessoa { Física, Jurídica }

    }
}