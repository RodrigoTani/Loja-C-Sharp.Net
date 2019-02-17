using Dominio;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class EstoqueProdutos : EntidadeDominio
    {
        public AdicionarJogo Jogo { get; set; }
         
        [Required]
        [Display(Name = "Porcentagem de precificação do Jogo")]
        public decimal PorcentagemPrecificacao { get; set; }

        public Fornecedor Fornecedor { get; set; }

        [Required(ErrorMessage = "O custo de compra do Jogo é necessário")]
        [Display(Name = "Custo do Jogo")]
        [DataType(DataType.Currency)]
        public decimal Custo { get; set; }

        [Required(ErrorMessage = "A quantidade deve ser informada")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

    }
}