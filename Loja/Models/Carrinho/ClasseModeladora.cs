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
        public void Preencher(int? idPedido)
        {
            PedidoId = idPedido;
            BuscarPedido();
            BuscarDetalhesPedidos();
            BuscarEndereco();
            BuscarProduto();
        }
        public void BuscarProduto()
        {
            //Produto = DB.Produtoes.ToList();
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
            //EnderecoEntrega = DB.EnderecoEntregas.Where(x => x. == Pedido.Usuario).FirstOrDefault();
        }
        public void BuscarDetalhesPedidos()
        {
            DetalhesPedidos = DB.DetalhesPedidoes.Where(x => x.PedidoId == PedidoId).ToList();
        }
        public void BuscarPedido()
        {
            Pedido = DB.Pedidoes.Where(x => x.PedidoId == PedidoId).FirstOrDefault();
        }
    }
}