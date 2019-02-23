using Loja.Models;
using Loja.Models.Carrinho;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EComerceMVC.Models
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
        // Helper method to simplify shopping cart calls

        public static CarrinhoDeCompras GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Produto produto)
        {
            // Get the matching cart and album instances
            var cartItem = storeDB.Carrrinhos.SingleOrDefault(
                c => c.CarrinhoId == ShoppingCartId
                && c.JogoId == produto.Id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Carrinho
                {
                    JogoId = produto.Id,
                    CarrinhoId = ShoppingCartId,
                    Quantidade = 1,
                    DataCriacao = DateTime.Now
                };
                storeDB.Carrrinhos.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Quantidade++;
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = storeDB.Carrrinhos.Single(
                cart => cart.CarrinhoId == ShoppingCartId
                 && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                storeDB.Carrrinhos.Remove(cartItem);
                // Save changes
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carrrinhos.Where(
                cart => cart.CarrinhoId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carrrinhos.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
        }
        public List<Carrinho> GetCartItems()
        {
            return storeDB.Carrrinhos.Where(
                cart => cart.CarrinhoId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carrrinhos
                          where cartItems.CarrinhoId == ShoppingCartId
                          select (int?)cartItems.Quantidade).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in storeDB.Carrrinhos
                              where cartItems.CarrinhoId == ShoppingCartId
                              select (int?)cartItems.Quantidade *
                              cartItems.Jogo.ValorFinal).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Pedido pedido)
        {
            decimal orderTotal = 0;
            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new DetalhesPedido
                {
                    ProdutoId = item.ProdutoId,
                    PedidoId = pedido.PedidoId,
                    PrecoUnitario = item.Produto.ValorFinal,
                    Quantidade = item.Quantidade
                };
                storeDB.EstoqueProdutos.Add(new EstoqueProdutos() { Quantidade = -item.Quantidade, Id = item.ProdutoId });
                // Set the order total of the shopping cart
                orderTotal += (item.Quantidade * item.Produto.ValorFinal);

                storeDB.DetalhesPedidoes.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            pedido.Total = orderTotal;

            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return pedido.PedidoId;
        }
        // We're using HttpContextBase to allow access to cookies.
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
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public int UpdateCartCount(int id, int cartCount)
        {
            // Get the cart 
            var cartItem = storeDB.Carrrinhos.Single(
                cart => cart.CarrinhoId == ShoppingCartId
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
                    storeDB.Carrrinhos.Remove(cartItem);
                }
                // Save changes 
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carrrinhos.Where(
                c => c.CarrinhoId == ShoppingCartId);

            foreach (Carrinho item in shoppingCart)
            {
                item.CarrinhoId = userName;
            }
            storeDB.SaveChanges();
        }

    }
}