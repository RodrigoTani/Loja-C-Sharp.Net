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
    public class MotivoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Motivo
        public ActionResult Index()
        {
            var motivoes = db.Motivoes.Include(m => m.prodid);
            return View(motivoes.ToList());
        }

        // GET: Motivo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motivo motivo = db.Motivoes.Find(id);
            if (motivo == null)
            {
                return HttpNotFound();
            }
            return View(motivo);
        }

        // GET: Motivo/Create
        public ActionResult Create()
        {
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo");
            return View();
        }

        // POST: Motivo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DataMotivo,ProdutoId,Usuario,MotivoAtivacao,MotivoInativacao")] Motivo motivo)
        {
            if (ModelState.IsValid)
            {
                db.Motivoes.Add(motivo);
                Produto produto = db.Produtoes.Find(motivo.ProdutoId);
                produto.Ativo = false;
                db.SaveChanges();
                return RedirectToAction("Inativar","Produto");
            }

            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", motivo.ProdutoId);
            return View(motivo);
        }

        // GET: Motivo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motivo motivo = db.Motivoes.Find(id);
            if (motivo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", motivo.ProdutoId);
            return View(motivo);
        }

        // POST: Motivo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DataMotivo,ProdutoId,Usuario,MotivoAtivacao,MotivoInativacao")] Motivo motivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motivo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProdutoId = new SelectList(db.Produtoes, "Id", "Titulo", motivo.ProdutoId);
            return View(motivo);
        }

        // GET: Motivo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motivo motivo = db.Motivoes.Find(id);
            if (motivo == null)
            {
                return HttpNotFound();
            }
            return View(motivo);
        }

        // POST: Motivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Motivo motivo = db.Motivoes.Find(id);
            db.Motivoes.Remove(motivo);
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
