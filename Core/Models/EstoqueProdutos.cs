using Dominio;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Models
{
    public class EstoqueProdutos : EntidadeDominio
    {
        [ForeignKey("AdicionarJogo")]
        public int Produto { get; set; }

        [ForeignKey("fornecedor1")]
        public int Fornecedores { get; set; }

        [Required]
        [Display(Name = "Porcentagem de precificação do Jogo")]
        public Decimal PorcentagemPrecificacao { get; set; }

        

        [Required(ErrorMessage = "O custo de compra do Jogo é necessário")]
        [Display(Name = "Custo do Jogo")]
        [DataType(DataType.Currency)]
        public Decimal Custo { get; set; }

        [Required(ErrorMessage = "A quantidade deve ser informada")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        public bool Ativo { get; set; } = true;

        //Auxiliares

        public virtual Fornecedor fornecedor1 { get; set; }
        public virtual Produto AdicionarJogo { get; set; }
    }
}