﻿using Loja.Models;
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
        // Helper method q simplifica a chamada do carrinho

        public static CarrinhoDeCompras GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Produto produto)
        {
            //Pega o carrinho correspondente as instâncias do álbum
            var cartItem = storeDB.Carrinhoes.SingleOrDefault(
                c => c.CarrinhoId == ShoppingCartId
                && c.ProdutoId == produto.Id);

            if (cartItem == null)
            {
                // Cria um novo carrinho caso não exista um
                cartItem = new Carrinho.Carrinho
                {
                    ProdutoId = produto.Id,
                    CarrinhoId = ShoppingCartId,
                    Quantidade = 1,
                    DataCriacao = DateTime.Now
                };
                storeDB.Carrinhoes.Add(cartItem);
            }
            else
            {
                // Se o item já existe no carrinho, 
                // então adiciona mais um na quantidade
                cartItem.Quantidade++;
            }
            // Salvar mudanças
            storeDB.SaveChanges();
        } 
        public int RemoveFromCart(int id)
        {
            // Pega o carrinho
            var cartItem = storeDB.Carrinhoes.Single(
                cart => cart.CarrinhoId == ShoppingCartId
                 && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                storeDB.Carrinhoes.Remove(cartItem);
                // Salvar mudanças
                storeDB.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = storeDB.Carrinhoes.Where(
                cart => cart.CarrinhoId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carrinhoes.Remove(cartItem);
            }
            // Salvar mudanças
            storeDB.SaveChanges();
        } 
        public List<Carrinho.Carrinho> GetCartItems()
        {
            return storeDB.Carrinhoes.Where(
                cart => cart.CarrinhoId == ShoppingCartId).ToList();
        }
        public int GetCount()
        {
            // Conta os items do carrinho e soma todos
            int? count = (from cartItems in storeDB.Carrinhoes
                          where cartItems.CarrinhoId == ShoppingCartId
                          select (int?)cartItems.Quantidade).Sum();
            // Retorna 0 se tds entradas forem null
            return count ?? 0;
        }
        public decimal GetTotal()
        {
            // Multiplica o preço  
            // soma todos os preços para ter o cart total
            decimal? total = (from cartItems in storeDB.Carrinhoes
                              where cartItems.CarrinhoId == ShoppingCartId
                              select (int?)cartItems.Quantidade *
                              cartItems.Produto.ValorFinal).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Pedido pedido)
        {
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
            // Set the order's total to the orderTotal count
            pedido.Total = orderTotal;

            // Salva o pedido
            storeDB.SaveChanges();
            // esvazia o carrinho
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
            // Pega o carrinho
            var cartItem = storeDB.Carrinhoes.Single(
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
                    storeDB.Carrinhoes.Remove(cartItem);
                }
                // Salvar mudanças 
                storeDB.SaveChanges();
            }
            return itemCount;
        }

        // quando um usuario esta logado,migra seu carrinho
        // associado com seu nome de usuario
        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDB.Carrinhoes.Where(
                c => c.CarrinhoId == ShoppingCartId);

            foreach (Carrinho.Carrinho item in shoppingCart)
            {
                item.CarrinhoId = userName;
            }
            storeDB.SaveChanges();
        }

    }
}