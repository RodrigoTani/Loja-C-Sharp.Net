using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Endereco()
        {
            return View();
        }
        
        // GET: Cupom/Create
        public ActionResult Cupom()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cupom([Bind(Include = "Id,Codigo,Valor,Ativo,DataCadastro")] Cupom cupom)
        {
            cupom.DataCadastro = DateTime.Now;
            cupom.Ativo = true;

            new Core.Controle.Fachada().Inserir(cupom);
            return View(cupom);
        }

        public ActionResult ListagemCupom()
        {
            return View();
        }
    }
}