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
        public bool Ativo { get; set; } = true;

        //Auxiliares

        public virtual EnderecoEntrega EnderecoEntrega { get; set; }
        public enum TipoPessoa { Física, Jurídica }

    }
}