using Core;
using Loja.Models;
using Loja.Models.Carrinho;
using System;
using System.Collections.Generic;
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
        [HttpPost]
        public ActionResult ClienteFormadePagamento2(string pagamento, int? id, int? cartao, decimal? valor)
        {
            decimal dec = 0;
            decimal.TryParse(Request.Form["dev"],out dec);
            valor = dec;
            return orc(pagamento, id, cartao, valor);
        }
        public static decimal valor;
        public ActionResult ClienteFormadePagamento(string pagamento, int? id, int? cartao, decimal? valor,bool? dife)
        {
            return orc(pagamento, id, cartao, valor);
        }
        public ActionResult orc(string pagamento, int? id, int? cartao, decimal? valor)
        {
            if (endere == 0)
            {
                //se o id recebido for nulo
                //manda cadastrar endereço
                //senão acha o endereço e salve-o
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
            valor = (valor == null) ? 0 : valor;
            var cart = CarrinhoDeCompras.GetCart(this.HttpContext);
            foreach (var r in storeDB.Cartaos.ToList())
            {
                if (Request.Form[r.Id.ToString()] != null)
                {
                    cartao = int.Parse(Request.Form[r.Id.ToString()]);
                    break;
                }
                
            }
            if (cartao == null)
            {
                return RedirectToAction("ListagemCartao");
            }
            decimal valord = (decimal)valor;
            Cartao card = storeDB.Cartaos.Find(cartao);
            var cards = new List<Pagamento>();//lista de cartoes
            //adiciona cartao na sessão
            //cria uma lista de obj cartoes na sessao
            if (Session["cards"]!=null)
            cards = (List<Pagamento>)Session["cards"];
            cards.Add(new Pagamento() { Cartaoid= card.Id,Valor=valord });
            Session["cards"] = cards;
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
            decimal mountcard = 0;
            foreach(Pagamento pag in cards)
            {
                mountcard+= pag.Valor;
            }
            // Retorna a view
            //viewModel.FormaPagamento = pagamento;
            viewModel.FormaPagamento = cartao.ToString();
            viewModel.EndId = endere;
            if(cart.GetTotal()==mountcard)
                return View("ClienteFormadePagamento", viewModel);
            else
                return RedirectToAction("ListagemCartao");
        }
      
        public ActionResult EscolhaEndereco()
        {
            return View(storeDB.EnderecoEntregas.ToList());
        }

        public ActionResult ListagemCartao()
        {
            return View(new Tuple<Pagamento,List<Cartao>>(new Pagamento(), storeDB.Cartaos.ToList()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClienteFormadePagamento(FormCollection values,int? id, int? cartao)
        {
            int dec = 0;
            int.TryParse(Request.Form["FormaPagamento"], out dec);
            cartao = dec;
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
                cart.CreateOrder(order,(List<Pagamento>)Session["cards"]);
                Session["cards"] = null;
                PedidoStatus stats = new PedidoStatus();
                stats.DataStatus = DateTime.Now;
                stats.PedidoId = order.PedidoId;
                stats.StatusId = 1;
                storeDB.PedidoStatus.Add(stats);
                storeDB.SaveChanges();

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
        /*
        // GET: Fornecedor/Edit/5
        public ActionResult Troca(int? id)
        {
           if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido ped = storeDB.Pedidoes.Find(id);
            if (ped == null)
            {
                return HttpNotFound();
            }
            return View(ped);
        }

        // POST: Fornecedor/Edit/5
        
        public ActionResult Troca(string motivo, int? id)
        {
            motivo = Request.Form["troca"];
            
            return View();
        }*/

    }
}