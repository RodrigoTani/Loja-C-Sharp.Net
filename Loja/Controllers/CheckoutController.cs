using Loja.Models;
using Loja.Models.Carrinho;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        ApplicationDbContext storeDB = new ApplicationDbContext();
        static int endere;
        // GET: Checkout
        public ActionResult Index()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
            // Set up our ViewModel
            var viewModel = new CarrinhodeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
    
            };
            // Retorna a view

            return View(viewModel);
        }
        public ActionResult ClienteFormadePagamento(string pagamento,int? id,int? cartao)
        {
            if (endere == 0)
            {
                if (id == null)
                {
                    endere = 0;
                    return RedirectToAction("EscolhaEndereco");
                }
                else
                {
                    EnderecoEntrega end = storeDB.EnderecoEntregas.Find(id);
                    endere = id.Value;
                    if (end == null)
                    {
                        return HttpNotFound();
                    }
                }
            }
            
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
            
            if (cartao == null)
            {
                return RedirectToAction("ListagemCartao");
            }
            Cartao card = storeDB.Cartaos.Find(cartao);
            if (card == null)
            {
                return HttpNotFound();
            }
            // Set up our ViewModel
            var viewModel = new CarrinhodeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
               
            };
            // Retorna a view
            //viewModel.FormaPagamento = pagamento;
            viewModel.FormaPagamento = cartao.ToString();
            viewModel.EndId = endere;
            return View(viewModel);
        }
      
        public ActionResult EscolhaEndereco()
        {
            return View(storeDB.EnderecoEntregas.ToList());
        }

        public ActionResult ListagemCartao()
        {
            return View(storeDB.Cartaos.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClienteFormadePagamento(FormCollection values,int? id, int? cartao)
        {
            
            EnderecoEntrega end = storeDB.EnderecoEntregas.Find(endere);
            if (end == null)
            {
                return HttpNotFound();
            }
            Cartao card = storeDB.Cartaos.Find(cartao);
            if (card == null)
            {
                return HttpNotFound();
            }
            ViewBag.Clientes = storeDB.Users; 
            var order = new Pedido();
            TryUpdateModel(order);
            string forma = card.Id.ToString();
            try
            {
                order.FormaPagamento = forma;
                order.Usuario = User.Identity.Name;
                order.DataPedido = DateTime.Now;
                order.Endereco = end.Id;
                order.Total = CarrinhoDeCompras.GetCart(this.HttpContext).GetTotal();
                //Salva o Pedido
                storeDB.Pedidoes.Add(order);
                storeDB.SaveChanges();
                var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete",new { id = order.PedidoId });
            }
            catch
            {
                //Invalido - Devolve tela com erros
                return View(order);
            }

        }
        public ActionResult Complete(int id)
        {
            // Valida o pedido para o Usuário
            bool isValid = storeDB.Pedidoes.Any(
                o => o.PedidoId == id);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }


        public ActionResult EnderecoEntrega()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnderecoEntrega([Bind(Include = "CEP,Estado,Cidade,Bairro,Logradouro,Numero,Observacao,DataCadastro")] EnderecoEntrega localEntrega)
        {
            if (ModelState.IsValid)
            {
                localEntrega.DataCadastro = DateTime.Now;
                localEntrega.Usuario = User.Identity.Name;
                storeDB.EnderecoEntregas.Add(localEntrega);
                storeDB.SaveChanges();
                return RedirectToAction("ClienteFormadePagamento");
            }

            return View(localEntrega);
        }

    }
}