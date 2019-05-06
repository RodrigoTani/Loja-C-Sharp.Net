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
    public class PedidoStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PedidoStatus
        public ActionResult Index()
        {
            var pedidoStatus = db.PedidoStatus.Include(p => p.pedi).Include(p => p.statu);
            return View(pedidoStatus.ToList());
        }

        // GET: PedidoStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoStatus pedidoStatus = db.PedidoStatus.Find(id);
            if (pedidoStatus == null)
            {
                return HttpNotFound();
            }
            return View(pedidoStatus);
        }

        // GET: PedidoStatus/Create
        public ActionResult Create()
        {
            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "PedidoId");
            ViewBag.StatusId = new SelectList(db.Status, "id", "NomeStatus");
            return View();
        }

        // POST: PedidoStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,PedidoId,StatusId,Usuario,DataStatus")] PedidoStatus pedidoStatus)
        {
            if (ModelState.IsValid)
            {
                pedidoStatus.Usuario = User.Identity.Name;
                pedidoStatus.DataStatus = DateTime.Now;
                db.PedidoStatus.Add(pedidoStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "Usuario", pedidoStatus.PedidoId);
            ViewBag.StatusId = new SelectList(db.Status, "id", "NomeStatus", pedidoStatus.StatusId);
            return View(pedidoStatus);
        }

        // GET: PedidoStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoStatus pedidoStatus = db.PedidoStatus.Find(id);
            if (pedidoStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "Usuario", pedidoStatus.PedidoId);
            ViewBag.StatusId = new SelectList(db.Status, "id", "NomeStatus", pedidoStatus.StatusId);
            return View(pedidoStatus);
        }

        // POST: PedidoStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,PedidoId,StatusId,Usuario,DataStatus")] PedidoStatus pedidoStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "Usuario", pedidoStatus.PedidoId);
            ViewBag.StatusId = new SelectList(db.Status, "id", "NomeStatus", pedidoStatus.StatusId);
            return View(pedidoStatus);
        }

        // GET: PedidoStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoStatus pedidoStatus = db.PedidoStatus.Find(id);
            if (pedidoStatus == null)
            {
                return HttpNotFound();
            }
            return View(pedidoStatus);
        }

        // POST: PedidoStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoStatus pedidoStatus = db.PedidoStatus.Find(id);
            db.PedidoStatus.Remove(pedidoStatus);
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
