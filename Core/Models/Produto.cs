using Dominio;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loja.Models
{
    public class Produto : EntidadeDominio
    {

        [Required]
        [Display(Name = "Titulo do Jogo")]
        public String Titulo { get; set; }

        [Display(Name = "Link Imagem do Jogo")]
        public String ImagemUrl { get; set; }

        [Required]
        public Plataforma plataforma{ get; set; }

        [Required]
        public Idioma idioma { get; set; }

        [Required]
        public Legenda legenda { get; set; }

        [Required]
        [Display(Name = "Dimensão do Jogo")]
        public String DimensaodoProduto { get; set; }

        [Required]
        public Genero genero { get; set; }

        [Required]
        public ClassificacaoIndicativa classificacaoIndicativa { get; set; }

        [Required]
        [Display(Name = "Desenvolvedor do Jogo")]
        public String Desenvolvedor { get; set; }

        [Required]
        [Display(Name = "Espaço Requerido no Hd do Jogo")]
        public String EspacoHd { get; set; }

        [Required]
        [Display(Name = "Conteúdo da Emabalagem")]
        public String ConteudoEmbalagem { get; set; }

        [Required]
        [ForeignKey("forn")]
        public int Fornecedor { get; set; }

        [Required]
        [Display(Name = "Garantia do Fornecedor")]
        public String GarantiaFornecedor { get; set; }

        [Required]
        [Display(Name = "Descrição1 do jogo")]
        public String Descricao1 { get; set; }
        [Required]
        [Display(Name = "Descrição2 do jogo")]
        public String Descricao2 { get; set; }

        [Required]
        public decimal ValorFinal { get; set; }
        [Required]
        public bool Ativo { get; set; } = true;

        // Auxiliares

        public virtual Fornecedor forn { get; set; }

        public enum Plataforma { PS4, XONE, PSVITA, SWITCH, PC }

        public enum Idioma { Português, Inglês, Espanhol, Japonês, Francês, Alemão }

        public enum Legenda { Português, Inglês, Espanhol, Japonês, Francês, Alemão }

        public enum Genero { Ação, Aventura, Estratégia, RPG, Esporte, Corrida, Tiro }

        public enum ClassificacaoIndicativa { L, _10, _12, _14, _16, _18 }

        //Regra de negocio
       

    }
}