using Dominio;
using System;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class Fornecedor : EntidadeDominio
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public String RazaoSocial { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        public TipoPessoa tipoPessoa { get; set; }
        public String CNPJ { get; set; }
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