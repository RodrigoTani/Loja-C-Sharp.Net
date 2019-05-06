using Core;
using Loja.Models;
using Loja.Models.Carrinho;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Models
{
    public class CarrinhoDeCompras
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static CarrinhoDeCompras GetCart(HttpContextBase context)
        {
            var cart = new CarrinhoDeCompras();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method q simplifica a chamada do ItemVenda

        public static CarrinhoDeCompras GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Produto produto)
        {
            //Pega o ItemVenda correspondente as instâncias do álbum
            var cartItem = storeDB.ItemVendaes.SingleOrDefault(
                c => c.ItemVendaId == ShoppingCartId
                && c.ProdutoId == produto.Id);

            if (cartItem == null)
            {
                // Cria um novo ItemVenda caso não exista um
                cartItem = new Carrinho.ItemVenda
                {
                    ProdutoId = produto.Id,
                    ItemVendaId = ShoppingCartId,
                    Quantidade = 1,
                    DataCriacao = DateTime.Now
                };
                storeDB.ItemVendaes.Add(cartItem);
            }
            else
            {
                // Se o item já existe no ItemVenda, 
                // então adiciona mais um na quantidade
                cartItem.Quantidade++;
            }
            // Salvar mudanças
            storeDB.SaveChanges();
        } 
        public int RemoveFromCart(int id)
        {
            // Pega o ItemVenda
            var cartItem = storeDB.ItemVendaes.Single(
                cart => cart.ItemVendaId == ShoppingCartId
                 && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                storeDB.ItemVendaes.Remove(cartItem);
                // Salvar mudanças
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.ItemVendaes.Where(
                cart => cart.ItemVendaId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.ItemVendaes.Remove(cartItem);
            }
            // Salvar mudanças
            storeDB.SaveChanges();
        } 
        public List<Carrinho.ItemVenda> GetCartItems()
        {
            return storeDB.ItemVendaes.Where(
                cart => cart.ItemVendaId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Conta os items do ItemVenda e soma todos
            int? count = (from cartItems in storeDB.ItemVendaes
                          where cartItems.ItemVendaId == ShoppingCartId
                          select (int?)cartItems.Quantidade).Sum();
            // Retorna 0 se tds entradas forem null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiplica o preço  
            // soma todos os preços para ter o cart total
            decimal? total = (from cartItems in storeDB.ItemVendaes
                              where cartItems.ItemVendaId == ShoppingCartId
                              select (int?)cartItems.Quantidade *
                              cartItems.Produto.ValorFinal).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Pedido pedido,List<Pagamento> formas)
        {
            var compra = new Venda();
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            // Iterage com os items do carriho, 
            // adicionando o detalhes do pedido para cada
            foreach (var item in cartItems)
            {
                var orderDetail = new DetalhesPedido
                {
                    ProdutoId = item.ProdutoId,
                    PedidoId = pedido.PedidoId,
                    PrecoUnitario = item.Produto.ValorFinal,
                    Quantidade = item.Quantidade
                };
                var b = storeDB.Produtoes.First(d => d.Id == item.ProdutoId);
                storeDB.EstoqueProdutos.Add(new EstoqueProdutos() { Quantidade = item.Quantidade, DataCadastro = DateTime.Now, Produto = item.ProdutoId, Fornecedores = b.Fornecedor });
                // Set the order total of the shopping cart
                orderTotal += (item.Quantidade * item.Produto.ValorFinal);

                storeDB.DetalhesPedidoes.Add(orderDetail);

            }
            compra.ItemVendas = cartItems;
            // Set the order's total to the orderTotal count
            compra.Total = pedido.Total = orderTotal;
            compra.Formas = formas;
            storeDB.Venda.Add(compra);
            // Salva o pedido
            storeDB.SaveChanges();
            // esvazia o ItemVenda
            EmptyCart();
            // Returna o id do pedido para a confirmação
            return pedido.PedidoId;
        }
        //HttpContextBase para o acesso dos cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // cria um novo random GUID usando System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    //Envia o tempCartId de volta para o cliente como um cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
         
        public int UpdateCartCount(int id, int cartCount)
        {
            // Pega o ItemVenda
            var cartItem = storeDB.ItemVendaes.Single(
                cart => cart.ItemVendaId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartCount > 0)
                {
                    cartItem.Quantidade = cartCount;
                    itemCount = cartItem.Quantidade;
                }
                else
                {
                    storeDB.ItemVendaes.Remove(cartItem);
                }
                // Salvar mudanças 
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        // quando um usuario esta logado,migra seu ItemVenda
        // associado com seu nome de usuario
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.ItemVendaes.Where(
                c => c.ItemVendaId == ShoppingCartId);

            foreach (Carrinho.ItemVenda item in shoppingCart)
            {
                item.ItemVendaId = userName;
            }
            storeDB.SaveChanges();
        }

    }
}