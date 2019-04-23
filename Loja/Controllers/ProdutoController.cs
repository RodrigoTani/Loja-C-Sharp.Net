using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Loja.Models;
using PagedList;

namespace Loja.Controllers
{
    public class ProdutoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public PartialViewResult ProdutoP()
        {
            var produtos = from s in db.Produtoes.Where(x => x.Ativo)
                           select s;

            return PartialView(produtos.ToList());
        }
        public PartialViewResult QuantidadeEstoqueP(int? id)
        {
            EstoqueProdutos ep = new EstoqueProdutos();
            ApplicationDbContext ex = new ApplicationDbContext();
            if (ex.EstoqueProdutos.Where(x => x.Id == id).FirstOrDefault() != null)
                ViewBag.Quantidade = ex.EstoqueProdutos.Where(x => x.Id == id).Sum(x => x.Quantidade);
            else
                ViewBag.Quantidade = 0;

            return PartialView();
        }

        /*
        // GET: Produto
        public ActionResult Index()
        {
            var produtoes = db.Produtoes.Include(p => p.forn);
            return View(produtoes.ToList());
        }*/

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "Titulo" : "";
            ViewBag.DateParm = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null){
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var produtos = from s in db.Produtoes
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                produtos = produtos.Where(s => s.Titulo.ToUpper().Contains(searchString.ToUpper())
                                       || s.Titulo.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Titulo":
                    produtos = produtos.OrderByDescending(s => s.Titulo);
                    break;
                case "Data":
                    produtos = produtos.OrderBy(s => s.DataCadastro);
                    break;
                case "Data_desc":
                    produtos = produtos.OrderByDescending(s => s.DataCadastro);
                    break;
                default:
                    produtos = produtos.OrderBy(s => s.Id);
                    break;
            }


            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(produtos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            ViewBag.Fornecedor = new SelectList(db.Fornecedors, "Id", "RazaoSocial");
            return View();
        }

        // POST: Produto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,ImagemUrl,plataforma,idioma,legenda,DimensaodoProduto,genero,classificacaoIndicativa,Desenvolvedor,EspacoHd,ConteudoEmbalagem,Fornecedor,GarantiaFornecedor,Descricao1,Descricao2,ValorFinal,Ativo,DataCadastro")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                produto.Ativo = true;
                produto.DataCadastro = DateTime.Now;
                db.Produtoes.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Fornecedor = new SelectList(db.Fornecedors, "Id", "RazaoSocial", produto.Fornecedor);
            return View(produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Fornecedor = new SelectList(db.Fornecedors, "Id", "RazaoSocial", produto.Fornecedor);
            return View(produto);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,ImagemUrl,plataforma,idioma,legenda,DimensaodoProduto,genero,classificacaoIndicativa,Desenvolvedor,EspacoHd,ConteudoEmbalagem,Fornecedor,GarantiaFornecedor,Descricao1,Descricao2,ValorFinal,Ativo,DataCadastro")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Fornecedor = new SelectList(db.Fornecedors, "Id", "RazaoSocial", produto.Fornecedor);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            db.Produtoes.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*
        public ActionResult Inativar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtoes.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }
        
        // POST: Produto/Inativar/5
        [HttpPost, ActionName("Inativar")]
        [ValidateAntiForgeryToken]
        public ActionResult InativeConfirmed(int id)
        {
            Produto produto = db.Produtoes.Find(id);
            RedirectToAction("Motivo");
            produto.Ativo = false;
            db.Entry(produto).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        //---------------------------------------
        // GET: Motivo/Create
        public ActionResult Motivo()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo");
            return View();
        }

        // POST: Motivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Motivo([Bind(Include = "id,DataMotivo,ProdutoId,Usuario,MotivoAtivacao,MotivoInativacao")] Motivo motivo)
        {
            if (ModelState.IsValid)
            {
                db.Motivoes.Add(motivo);
                Produto produto = db.Produtoes.Find(motivo.ProdutoId);
                produto.Ativo = false;
                db.SaveChanges();
                return RedirectToAction("Index", "Produto");
            }

            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", motivo.ProdutoId);
            return View(motivo);
        }*/

        //----------------------------------
        // GET: Motivo/Create
        public ActionResult Inativar()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo");
            return View();
        }
        // POST: Motivo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inativar([Bind(Include = "id,DataMotivo,ProdutoId,Usuario,MotivoAtivacao,MotivoInativacao")] Motivo motivo)
        {
            if (ModelState.IsValid)
            {
                db.Motivoes.Add(motivo);
                Produto produto = db.Produtoes.Find(motivo.ProdutoId);
                motivo.DataMotivo = DateTime.Now;
                produto.Ativo = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", motivo.ProdutoId);
            return View(motivo);
        }
        //----------------------------------
        // GET: Motivo/Create
        public ActionResult Ativar()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo");
            return View();
        }

        // POST: Motivo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ativar([Bind(Include = "id,DataMotivo,ProdutoId,Usuario,MotivoAtivacao,MotivoInativacao")] Motivo motivo)
        {
            if (ModelState.IsValid)
            {
                db.Motivoes.Add(motivo);
                Produto produto = db.Produtoes.Find(motivo.ProdutoId);
                motivo.DataMotivo = DateTime.Now;
                produto.Ativo = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", motivo.ProdutoId);
            return View(motivo);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
