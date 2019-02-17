using Dominio;
using System;
using System.ComponentModel.DataAnnotations;

namespace Loja.Models
{
    public class AdicionarJogo : EntidadeDominio
    {

        [Required]
        [Display(Name = "Titulo do Jogo")]
        public String Titulo { get; set; }

        [Display(Name = "Link Imagem do Jogo")]
        public String ImagemUrl { get; set; }

        public enum Plataforma { PS4, XONE, PSVITA, SWITCH, PC }

        public enum Idioma { Português, Inglês, Espanhol, Japonês, Francês }

        public enum Legenda { Português, Inglês, Espanhol, Japonês, Francês }

        [Display(Name = "Dimensão do Jogo")]
        public String DimensaodoProduto { get; set; }

        public enum Gênero { Ação, Aventura, Estratégia, RPG, Esporte, Corrida, Tiro }

        public enum ClassificacaoIndicativa { L, _10, _12, _14, _16, _18 }

        [Display(Name = "Desenvolvedor do Jogo")]
        public String Desenvolvedor { get; set; }

        [Display(Name = "Espaço Requerido no Hd do Jogo")]
        public String EspacoHd { get; set; }

        [Display(Name = "Conteúdo da Emabalagem")]
        public String ConteudoEmbalagem { get; set; }

        public Fornecedor Fornecedor { get; set; }

        [Display(Name = "Garantia do Fornecedor")]
        public String GarantiaFornecedor { get; set; }

        [Display(Name = "Descrição1 do jogo")]
        public String Descricao1 { get; set; }

        [Display(Name = "Descrição2 do jogo")]
        public String Descricao2 { get; set; }

        public decimal ValorFinal;

        public EstoqueProdutos ep;

        public decimal CalculaValorFinal(EstoqueProdutos ep){ 
            return ep.PorcentagemPrecificacao * ep.Custo;
        }
        
        public AdicionarJogo() {

            ValorFinal = CalculaValorFinal(ep);

        }
    }
}