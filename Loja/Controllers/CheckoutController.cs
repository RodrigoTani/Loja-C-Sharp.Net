using Loja.Models;
using Loja.Models.Carrinho;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        ApplicationDbContext storeDB = new ApplicationDbContext();
        // GET: Checkout
        public ActionResult Index()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new CarrinhodeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Retorna a view

            return View(viewModel);
        }
        public ActionResult ClienteFormadePagamento(string pagamento)
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
            if (storeDB.EnderecoEntregas.Where(x => x.Usuario == User.Identity.Name).FirstOrDefault() == null)
            {
                return RedirectToAction("EnderecoEntrega");
            }
            // Set up our ViewModel
            var viewModel = new CarrinhodeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Retorna a view
            viewModel.FormaPagamento = pagamento;
            return View(viewModel);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClienteFormadePagamento(FormCollection values)
        {
            ViewBag.Clientes = storeDB.Users; 
            var order = new Pedido();
            TryUpdateModel(order);
            string forma = Request.Form["FormaPagamento"];
            try
            {
                order.FormaPagamento = forma;
                order.Usuario = User.Identity.Name;
                order.DataPedido = DateTime.Now;
                order.Total = CarrinhoDeCompras.GetCart(this.HttpContext).GetTotal();
                //Salva o Pedido
                storeDB.Pedidoes.Add(order);
                storeDB.SaveChanges();
                /*
                EstoqueProdutos e;
                e.Quantidade = e.Quantidade - 1;*/
                //Processa o pedido
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

    }
}