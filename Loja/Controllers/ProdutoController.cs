using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Loja.Models;

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


        // GET: Produto
        public ActionResult Index()
        {
            var produtoes = db.Produtoes.Include(p => p.forn);
            return View(produtoes.ToList());
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
