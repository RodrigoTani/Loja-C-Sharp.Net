using Core;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Loja.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Endereco()
        {
            return View();
        }

        public ActionResult Relatorio()
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

        public bool ValidaCupom (string cupom)
        {
            
            Cupom cup = db.Cupom.Where(x => x.Codigo == cupom).FirstOrDefault();
            if(cup.Ativo == true)
            {
                return true;
            }

            return false;
        }
    }
}