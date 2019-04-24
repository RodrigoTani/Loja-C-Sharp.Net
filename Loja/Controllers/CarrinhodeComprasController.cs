using Loja.Models;
using Loja.Models.Carrinho;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class CarrinhoDeComprasController : Controller
    {
        ApplicationDbContext storeDB = new ApplicationDbContext();
        //
        // GET: /ShoppingCart/
        

        public ActionResult Index()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
            // Set up our ViewModel
            var viewModel = new CarrinhodeComprasViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal(),
               
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5



        public ActionResult AddToCart(int id)
        {
            // Recupera o álbum do banco de dados
            var addProduto = storeDB.Produtoes
                .Single(produto => produto.Id == id);

            // Adiciona ao carrinho de compras
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            cart.AddToCart(addProduto);

            // Volta para a página da loja principal para mais compras
            return RedirectToAction("Index", "Home", null);
        }
        [HttpPost]
        public ActionResult UpdateCartCount(int id, int cartCount)
        {
            CarrinhodeComprasRemoverViewModel results = null;
            try
            {
                // Obtein o Carrinho
                var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

                // Pega o nome do álbum para exibir a confirmação
                string albumName = storeDB.Carrinhoes.Single(item => item.RecordId == id).Produto.Titulo;

                // Update a contagem do carrinho
                int itemCount = cart.UpdateCartCount(id, cartCount);

                //Prepara menssages
                string msg = "A quantidade de " + Server.HtmlEncode(albumName) +
                        " foi atualizada no carrinho";
                if (itemCount == 0) msg = Server.HtmlEncode(albumName) +
                        " foi removida do carrinho.";
                //
                // Mostra mensagem de confirmação 
                results = new CarrinhodeComprasRemoverViewModel
                {
                    Message = msg,
                    CartTotal = cart.GetTotal(),
                    CartCount = cart.GetCount(),
                    ItemCount = itemCount,
                    DeleteId = id
                };
            }
            catch
            {
                results = new CarrinhodeComprasRemoverViewModel
                {
                    Message = "Erro ocorreu ou entrada inválida...",
                    CartTotal = -1,
                    CartCount = -1,
                    ItemCount = -1,
                    DeleteId = id
                };
            }
            return Json(results);
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove o item do carrinho
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            // Pega o nome do álbum para exibir a confirmação
            string nomeProduto = storeDB.Carrinhoes
                .Single(item => item.RecordId == id).Produto.Titulo;

            // Remove do carrinho
            int itemCount = cart.RemoveFromCart(id);

            // Mostra a confirmação da mensagem
            var results = new CarrinhodeComprasRemoverViewModel
            {
                Message = Server.HtmlEncode(nomeProduto) +
                    " Foi removido do carrinho.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
        [Authorize]
        public PartialViewResult Pagamento()
        {
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);


            return PartialView(cart);
        }

        // POST: Pedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult Pagamento(Carrinho cart)
        {
            if (ModelState.IsValid)
            {
                storeDB.Entry(cart).State = EntityState.Modified;
                storeDB.SaveChanges();
            }
            return PartialView(cart);
        }

    }

}
