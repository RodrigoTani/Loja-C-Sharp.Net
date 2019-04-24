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
    public class MotivoFornController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MotivoForn
        public ActionResult Index()
        {
            var motivoForns = db.MotivoForns.Include(m => m.forn);
            return View(motivoForns.ToList());
        }

        // GET: MotivoForn/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoForn motivoForn = db.MotivoForns.Find(id);
            if (motivoForn == null)
            {
                return HttpNotFound();
            }
            return View(motivoForn);
        }

        // GET: MotivoForn/Create
        public ActionResult Create()
        {
            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial");
            return View();
        }

        // POST: MotivoForn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,DataMotivo,fornecedo,Usuario,MotivoAtivacao,MotivoInativacao")] MotivoForn motivoForn)
        {
            if (ModelState.IsValid)
            {
                db.MotivoForns.Add(motivoForn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial", motivoForn.fornecedo);
            return View(motivoForn);
        }

        // GET: MotivoForn/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoForn motivoForn = db.MotivoForns.Find(id);
            if (motivoForn == null)
            {
                return HttpNotFound();
            }
            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial", motivoForn.fornecedo);
            return View(motivoForn);
        }

        // POST: MotivoForn/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,DataMotivo,fornecedo,Usuario,MotivoAtivacao,MotivoInativacao")] MotivoForn motivoForn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(motivoForn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial", motivoForn.fornecedo);
            return View(motivoForn);
        }

        // GET: MotivoForn/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MotivoForn motivoForn = db.MotivoForns.Find(id);
            if (motivoForn == null)
            {
                return HttpNotFound();
            }
            return View(motivoForn);
        }

        // POST: MotivoForn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MotivoForn motivoForn = db.MotivoForns.Find(id);
            db.MotivoForns.Remove(motivoForn);
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

        // GET: MotivoForn/Create
        public ActionResult Inativar()
        {
            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial");
            return View();
        }

        // POST: MotivoForn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inativar([Bind(Include = "id,DataMotivo,fornecedo,Usuario,MotivoAtivacao,MotivoInativacao")] MotivoForn motivoForn)
        {
            if (ModelState.IsValid)
            {
                db.MotivoForns.Add(motivoForn);
                Fornecedor fornecedor  = db.Fornecedors.Find(motivoForn.id);
                motivoForn.Usuario = User.Identity.Name;
                motivoForn.DataMotivo = DateTime.Now;
                fornecedor.Ativo = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial", motivoForn.fornecedo);
            return View(motivoForn);
        }

        // GET: MotivoForn/Create
        public ActionResult Anativar()
        {
            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial");
            return View();
        }

        // POST: MotivoForn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Anativar([Bind(Include = "id,DataMotivo,fornecedo,Usuario,MotivoAtivacao,MotivoInativacao")] MotivoForn motivoForn)
        {
            if (ModelState.IsValid)
            {
                db.MotivoForns.Add(motivoForn);
                Fornecedor fornecedor = db.Fornecedors.Find(motivoForn.id);
                motivoForn.Usuario = User.Identity.Name;
                motivoForn.DataMotivo = DateTime.Now;
                fornecedor.Ativo = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial", motivoForn.fornecedo);
            return View(motivoForn);
        }

    }
}
