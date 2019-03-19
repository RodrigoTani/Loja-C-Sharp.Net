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
            // Return the view

            return View(viewModel);
        }
        public ActionResult ClienteEFormaPagamento(string pagamento)
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
            if (storeDB.EnderecoEntregas.Where(x => x.Usuario == User.Identity.Name).FirstOrDefault() == null)
            {
                return RedirectToAction("CadastrarEndereço");
            }
            // Set up our ViewModel
            var viewModel = new CarrinhodeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            viewModel.FormaPagamento = pagamento;
            return View(viewModel);
        }
        public ActionResult CadastrarEndereço()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastrarEndereço([Bind(Include = "Telefone,Celular,CEP,Estado,Bairro,Cidade,Endereco,Numero")] EnderecoEntrega localEntrega)
        {
            if (ModelState.IsValid)
            {
                localEntrega.Usuario = User.Identity.Name;
                storeDB.EnderecoEntregas.Add(localEntrega);
                storeDB.SaveChanges();
                return RedirectToAction("ClienteEFormaPagamento");
            }

            return View(localEntrega); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClienteEFormaPagamento(FormCollection values)
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
                //Save Order
                storeDB.Pedidoes.Add(order);
                storeDB.SaveChanges();
                //Process the order
                var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
                cart.CreateOrder(order);

                return RedirectToAction("Complete",
                        new { id = order.PedidoId });
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }

        }
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
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