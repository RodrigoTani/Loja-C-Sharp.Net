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
using Core;

namespace Loja.Controllers
{
    public class ItemVendaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ItemVenda
        public ActionResult Index()
        {
            var ItemVendaes = db.ItemVendaes.Include(c => c.Produto);
            return View(ItemVendaes.ToList());
        }

        // GET: ItemVenda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda ItemVenda = db.ItemVendaes.Find(id);
            if (ItemVenda == null)
            {
                return HttpNotFound();
            }
            return View(ItemVenda);
        }

        // GET: ItemVenda/Create
        public ActionResult Create()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo");
            return View();
        }

        // POST: ItemVenda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RecordId,ItemVendaId,Quantidade,DataCriacao,ProdutoId,FormaPagamento")] ItemVenda ItemVenda)
        {
            if (ModelState.IsValid)
            {
                db.ItemVendaes.Add(ItemVenda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", ItemVenda.ProdutoId);
            return View(ItemVenda);
        }

        // GET: ItemVenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda ItemVenda = db.ItemVendaes.Find(id);
            if (ItemVenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", ItemVenda.ProdutoId);
            return View(ItemVenda);
        }

        // POST: ItemVenda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RecordId,ItemVendaId,Quantidade,DataCriacao,ProdutoId,FormaPagamento")] ItemVenda ItemVenda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ItemVenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", ItemVenda.ProdutoId);
            return View(ItemVenda);
        }

        // GET: ItemVenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemVenda ItemVenda = db.ItemVendaes.Find(id);
            if (ItemVenda == null)
            {
                return HttpNotFound();
            }
            return View(ItemVenda);
        }

        // POST: ItemVenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemVenda ItemVenda = db.ItemVendaes.Find(id);
            db.ItemVendaes.Remove(ItemVenda);
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
