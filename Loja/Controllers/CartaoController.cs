using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Loja.Models;
using Core;

namespace Loja.Controllers
{
    public class CartaoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext(); 

        // GET: Cartao
        public ActionResult Index()
        {
            return View(db.Cartaos.ToList());
        }

        // GET: Cartao/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartao cartao = db.Cartaos.Find(id);
            if (cartao == null)
            {
                return HttpNotFound();
            }
            return View(cartao);
        }

        // GET: Cartao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cartao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Usuario,NomeCartao,NumeroCartao,DataExpira,CVV,DataCadastro")] Cartao cartao)
        {
            if (ModelState.IsValid)
            {
                cartao.Usuario = User.Identity.Name;
                cartao.DataCadastro = DateTime.Now;
                db.Cartaos.Add(cartao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartao);
        }

        // GET: Cartao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartao cartao = db.Cartaos.Find(id);
            if (cartao == null)
            {
                return HttpNotFound();
            }
            return View(cartao);
        }

        // POST: Cartao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Usuario,NomeCartao,NumeroCartao,DataExpira,CVV,DataCadastro")] Cartao cartao)
        {
            if (ModelState.IsValid)
            {
                cartao.Usuario = User.Identity.Name;
                cartao.DataCadastro = DateTime.Now;
                db.Entry(cartao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartao);
        }

        // GET: Cartao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cartao cartao = db.Cartaos.Find(id);
            if (cartao == null)
            {
                return HttpNotFound();
            }
            return View(cartao);
        }

        // POST: Cartao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cartao cartao = db.Cartaos.Find(id);
            db.Cartaos.Remove(cartao);
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
