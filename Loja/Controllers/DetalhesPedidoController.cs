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
    public class DetalhesPedidoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DetalhesPedido
        public ActionResult Index()
        {
            return View(db.DetalhesPedidoes.ToList());
        }

        // GET: DetalhesPedido/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalhesPedido detalhesPedido = db.DetalhesPedidoes.Find(id);
            if (detalhesPedido == null)
            {
                return HttpNotFound();
            }
            return View(detalhesPedido);
        }

        // GET: DetalhesPedido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DetalhesPedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DetalhesPedidoId,PedidoId,ProdutoId,Quantidade,PrecoUnitario")] DetalhesPedido detalhesPedido)
        {
            if (ModelState.IsValid)
            {
                db.DetalhesPedidoes.Add(detalhesPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(detalhesPedido);
        }

        // GET: DetalhesPedido/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalhesPedido detalhesPedido = db.DetalhesPedidoes.Find(id);
            if (detalhesPedido == null)
            {
                return HttpNotFound();
            }
            return View(detalhesPedido);
        }

        // POST: DetalhesPedido/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DetalhesPedidoId,PedidoId,ProdutoId,Quantidade,PrecoUnitario")] DetalhesPedido detalhesPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalhesPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(detalhesPedido);
        }

        // GET: DetalhesPedido/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalhesPedido detalhesPedido = db.DetalhesPedidoes.Find(id);
            if (detalhesPedido == null)
            {
                return HttpNotFound();
            }
            return View(detalhesPedido);
        }

        // POST: DetalhesPedido/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalhesPedido detalhesPedido = db.DetalhesPedidoes.Find(id);
            db.DetalhesPedidoes.Remove(detalhesPedido);
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
