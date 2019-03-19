using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Loja.Models;
using Loja.Models.Carrinho;

namespace Loja.Controllers
{
    public class CarrinhoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carrinho
        public ActionResult Index()
        {
            var carrinhoes = db.Carrinhoes.Include(c => c.Produto);
            return View(carrinhoes.ToList());
        }

        // GET: Carrinho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = db.Carrinhoes.Find(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // GET: Carrinho/Create
        public ActionResult Create()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo");
            return View();
        }

        // POST: Carrinho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,CarrinhoId,Quantidade,DataCriacao,ProdutoId,FormaPagamento")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                db.Carrinhoes.Add(carrinho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", carrinho.ProdutoId);
            return View(carrinho);
        }

        // GET: Carrinho/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = db.Carrinhoes.Find(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", carrinho.ProdutoId);
            return View(carrinho);
        }

        // POST: Carrinho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,CarrinhoId,Quantidade,DataCriacao,ProdutoId,FormaPagamento")] Carrinho carrinho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carrinho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", carrinho.ProdutoId);
            return View(carrinho);
        }

        // GET: Carrinho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carrinho carrinho = db.Carrinhoes.Find(id);
            if (carrinho == null)
            {
                return HttpNotFound();
            }
            return View(carrinho);
        }

        // POST: Carrinho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Carrinho carrinho = db.Carrinhoes.Find(id);
            db.Carrinhoes.Remove(carrinho);
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
