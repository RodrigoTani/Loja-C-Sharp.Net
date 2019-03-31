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
    }
}