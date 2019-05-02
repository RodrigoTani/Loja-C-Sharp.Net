using System.Collections.Generic;
using System.Linq;

namespace Loja.Models.Carrinho
{
    public class ClasseModeladora
    {
        public int ProdutoId { get; set; }
        public int? PedidoId { get; set; }
        public string DonoEndereco { get; set; }
        public List<DetalhesPedido> DetalhesPedidos { get; set; }
        public List<Produto> Produto { get; set; }
        public Pedido Pedido { get; set; }
        public ApplicationDbContext DB { get; set; }
        public EnderecoEntrega EnderecoEntrega { get; set; }
        public List<Cartao> cartao { get; set; }
        public void Preencher(int? idPedido)
        {
            PedidoId = idPedido;
            BuscarPedido();
            BuscarDetalhesPedidos();
            BuscarEndereco();
            BuscarProduto();
            BuscarPagamento();
        }
        public void BuscarProduto()
        {
            Produto = DB.Produtoes.ToList();
            List<Produto> lsProd = new List<Produto>();
            foreach (var i in DetalhesPedidos)
            {
                Produto p = Produto.Where(x => x.Id == i.ProdutoId).FirstOrDefault();
                lsProd.Add(p);
            }
            Produto = lsProd;
        }
        public void BuscarEndereco()
        {
            EnderecoEntrega = DB.EnderecoEntregas.Where(x => x.Usuario == Pedido.Usuario && Pedido.Endereco == x.Id).FirstOrDefault(); 
        }
        public void BuscarDetalhesPedidos()
        {
            DetalhesPedidos = DB.DetalhesPedidoes.Where(x => x.PedidoId == PedidoId).ToList();
        }
        public void BuscarPedido()
        {
            Pedido = DB.Pedidoes.Where(x => x.PedidoId == PedidoId).FirstOrDefault();
        }
        public void BuscarPagamento()
        {
            cartao = DB.Cartaos.ToList();
            List<Cartao> lsProd = new List<Cartao>();
            foreach (var i in cartao)
            {
                Cartao c = cartao.Where(x => x.Id == i.Id).FirstOrDefault();
                lsProd.Add(c);
            }
            cartao = lsProd;
        }

    }   
}