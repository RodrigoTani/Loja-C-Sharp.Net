using Core;
using System;
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
        public List<PedidoStatus> status { get; set; }
        public List<ItemVenda> pag { get; set; }
        public void Preencher(int? idPedido)
        {
            PedidoId = idPedido;
            BuscarPedido();
            BuscarDetalhesPedidos();
            BuscarEndereco();
            BuscarProduto();
            BuscarPagamento();
            BuscarStatus();
            BuscarPag();
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
        public void BuscarPag()
        {
            pag = DB.ItemVendaes.ToList();
            List<ItemVenda> lsProd = new List<ItemVenda>();
            foreach (var i in pag)
            {
                ItemVenda c = pag.Where(x => x.FormaPagamento == i.FormaPagamento).FirstOrDefault();
                lsProd.Add(c);
            }
            pag = lsProd;
        }
        public void BuscarStatus()
        {
            status = DB.PedidoStatus.ToList();
            List<PedidoStatus> lsProd = new List<PedidoStatus>();
            foreach (var i in status)
            {
                PedidoStatus c = status.Where(x => x.PedidoId == i.pedi.PedidoId && x.id == i.id).FirstOrDefault();
                lsProd.Add(c);
            }
            status = lsProd;
        }

    }   
}