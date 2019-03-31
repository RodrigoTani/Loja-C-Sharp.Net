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
    public class EstoqueProdutosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EstoqueProdutos
        public ActionResult Index()
        {
            var estoqueProdutos = db.EstoqueProdutos.Include(e => e.AdicionarJogo).Include(e => e.fornecedor1);
            return View(estoqueProdutos.ToList());
        }

        // GET: EstoqueProdutos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueProdutos estoqueProdutos = db.EstoqueProdutos.Find(id);
            if (estoqueProdutos == null)
            {
                return HttpNotFound();
            }
            return View(estoqueProdutos);
        }

        // GET: EstoqueProdutos/Create
        public ActionResult Create()
        {
            ViewBag.Produto = new SelectList(db.Produtoes, "Id", "Titulo");
            ViewBag.Fornecedores = new SelectList(db.Fornecedors, "Id", "RazaoSocial");
            return View();
        }

        // POST: EstoqueProdutos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Produto,Fornecedores,PorcentagemPrecificacao,Custo,Quantidade,Ativo,DataCadastro")] EstoqueProdutos estoqueProdutos)
        {
            if (ModelState.IsValid)
            {
                estoqueProdutos.Ativo = true;
                estoqueProdutos.DataCadastro = DateTime.Now;
                db.EstoqueProdutos.Add(estoqueProdutos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Produto = new SelectList(db.Produtoes, "Id", "Titulo", estoqueProdutos.Produto);
            ViewBag.Fornecedores = new SelectList(db.Fornecedors, "Id", "RazaoSocial", estoqueProdutos.Fornecedores);
            return View(estoqueProdutos);
        }

        // GET: EstoqueProdutos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueProdutos estoqueProdutos = db.EstoqueProdutos.Find(id);
            if (estoqueProdutos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Produto = new SelectList(db.Produtoes, "Id", "Titulo", estoqueProdutos.Produto);
            ViewBag.Fornecedores = new SelectList(db.Fornecedors, "Id", "RazaoSocial", estoqueProdutos.Fornecedores);
            return View(estoqueProdutos);
        }

        // POST: EstoqueProdutos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Produto,Fornecedores,PorcentagemPrecificacao,Custo,Quantidade,Ativo,DataCadastro")] EstoqueProdutos estoqueProdutos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estoqueProdutos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Produto = new SelectList(db.Produtoes, "Id", "Titulo", estoqueProdutos.Produto);
            ViewBag.Fornecedores = new SelectList(db.Fornecedors, "Id", "RazaoSocial", estoqueProdutos.Fornecedores);
            return View(estoqueProdutos);
        }

        // GET: EstoqueProdutos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstoqueProdutos estoqueProdutos = db.EstoqueProdutos.Find(id);
            if (estoqueProdutos == null)
            {
                return HttpNotFound();
            }
            return View(estoqueProdutos);
        }

        // POST: EstoqueProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstoqueProdutos estoqueProdutos = db.EstoqueProdutos.Find(id);
            db.EstoqueProdutos.Remove(estoqueProdutos);
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
