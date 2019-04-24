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
    public class FornecedorController : Controller
    {
            private ApplicationDbContext db = new ApplicationDbContext();

            public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "RazaoSocial" : "";
                ViewBag.DateParm = sortOrder == "Date" ? "Date_desc" : "Date";


                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;

                var fornecedores = from s in db.Fornecedors
                                   select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    fornecedores = fornecedores.Where(s => s.RazaoSocial.ToUpper().Contains(searchString.ToUpper())
                                           || s.RazaoSocial.ToUpper().Contains(searchString.ToUpper()));
                }
                switch (sortOrder)
                {
                    case "RazaoSocial":
                        fornecedores = fornecedores.OrderByDescending(s => s.RazaoSocial);
                        break;
                    case "Data":
                        fornecedores = fornecedores.OrderBy(s => s.DataCadastro);
                        break;
                    case "Data_desc":
                        fornecedores = fornecedores.OrderByDescending(s => s.DataCadastro);
                        break;
                    default:
                        fornecedores = fornecedores.OrderBy(s => s.CNPJ);
                        break;
                }


                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(fornecedores.ToPagedList(pageNumber, pageSize));
            }




            // GET: Fornecedor/Details/5
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Fornecedor fornecedor = db.Fornecedors.Find(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }

            // GET: Fornecedor/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Fornecedor/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create([Bind(Include = "Id,Nome,tipoPessoa,CNPJ,RazaoSocial,Ativo,DataCadastro")] Fornecedor fornecedor)
            {
                if (ModelState.IsValid)
                {
                    fornecedor.Ativo = true;
                    fornecedor.DataCadastro = DateTime.Now;
                    db.Fornecedors.Add(fornecedor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(fornecedor);
            }

            // GET: Fornecedor/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Fornecedor fornecedor = db.Fornecedors.Find(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }

            // POST: Fornecedor/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,Nome,tipoPessoa,CNPJ,RazaoSocial,Ativo,DataCadastro")] Fornecedor fornecedor)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(fornecedor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(fornecedor);
            }

            // GET: Fornecedor/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Fornecedor fornecedor = db.Fornecedors.Find(id);
                if (fornecedor == null)
                {
                    return HttpNotFound();
                }
                return View(fornecedor);
            }

            // POST: Fornecedor/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Fornecedor fornecedor = db.Fornecedors.Find(id);
                db.Fornecedors.Remove(fornecedor);
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
                Fornecedor fornecedor = db.Fornecedors.Find(motivoForn.id);
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
        public ActionResult Ativar()
        {
            ViewBag.fornecedo = new SelectList(db.Fornecedors, "Id", "RazaoSocial");
            return View();
        }

        // POST: MotivoForn/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ativar([Bind(Include = "id,DataMotivo,fornecedo,Usuario,MotivoAtivacao,MotivoInativacao")] MotivoForn motivoForn)
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