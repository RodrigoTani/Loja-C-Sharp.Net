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

        public static int b = 0;
        public ActionResult AddDepartamento()
        {
            new Core.Controle.Fachada().Inserir(new Dominio.Departamento() {Id=b++,Nome_Departamento ="slipknot",DataCadastro = DateTime.Now });
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
        /*
        // GET: Cupom/Create
        public ActionResult Cupom()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cupom([Bind(Include = "Id,Codigo,Valor,Ativo,DataCadastro")] Cupom cupom)
        {
            if (ModelState.IsValid)
            {
                cupom.Ativo = true;
                cupom.DataCadastro = DateTime.Now;
                db.Fornecedors.Add(cupom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            new Core.Controle.Fachada().Inserir(new Dominio.Cupom() {
                Id ,
                Codigo,
                Valor,
                DataCadastro = DateTime.Now,
                Ativo = true

            });
            return View(cupom);
        }*/
    }
}