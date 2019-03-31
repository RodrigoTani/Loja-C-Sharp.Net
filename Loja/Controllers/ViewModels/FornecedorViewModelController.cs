using Loja.Models.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;
using Loja.Models;
using PagedList;

namespace Loja.Controllers.ViewModels
{
    public class FornecedorViewModelController : Controller
    {
        private Models.ApplicationDbContext db = new ApplicationDbContext();
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "RazaoSocial" : "";
            ViewBag.DateParm = sortOrder == "Date" ? "Date_desc" : "Date";
            var viewModel = new FornecedorViewModel();

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var fornecedores = from s in db.Fornecedors
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                fornecedores = fornecedores.Where(s => s.RazaoSocial.ToUpper().Contains(searchString.ToUpper())
                                       || s.RazaoSocial.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "RazaoSocial":
                    fornecedores = fornecedores.OrderByDescending(s => s.RazaoSocial);
                    break;
                case "Data":
                    fornecedores = fornecedores.OrderBy(s => s.DataCadastro);
                    break;
                case "Data_desc":
                    fornecedores = fornecedores.OrderByDescending(s => s.DataCadastro);
                    break;
                default:
                    fornecedores = fornecedores.OrderBy(s => s.CNPJ);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(fornecedores.ToPagedList(pageNumber, pageSize));
        }
    }
}