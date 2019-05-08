using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Loja.Models.Carrinho;
using Core;
using PagedList;

namespace Loja.Controllers
{
    public class PedidoStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PedidoStatus

        //-----------------------------
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeParam = String.IsNullOrEmpty(sortOrder) ? "PedidoId" : "";
            ViewBag.NomeStatus = String.IsNullOrEmpty(sortOrder) ? "Status" : "";
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

            var pedidoStatus = from s in db.PedidoStatus
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                pedidoStatus = pedidoStatus.Where(s => s.PedidoId.ToString().ToUpper().Contains(searchString.ToUpper())
                                       || s.PedidoId.ToString().ToUpper().Contains(searchString.ToUpper())
                                       || s.StatusId.ToString().ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "PedidoId":
                    pedidoStatus = pedidoStatus.OrderByDescending(s => s.PedidoId);
                    break;
                case "Status":
                    pedidoStatus = pedidoStatus.OrderByDescending(s => s.StatusId);
                    break;
                case "Data":
                    pedidoStatus = pedidoStatus.OrderBy(s => s.DataStatus);
                    break;
                case "Data_desc":
                    pedidoStatus = pedidoStatus.OrderByDescending(s => s.DataStatus);
                    break;
                default:
                    pedidoStatus = pedidoStatus.OrderBy(s => s.PedidoId);
                    break;
            }


            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(pedidoStatus.ToPagedList(pageNumber, pageSize));
        }
        //-----------------------------


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

        public ActionResult TrocaPedido(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = pedido.PedidoId;
            return View();
        }

        // POST: PedidoStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TrocaPedido(PedidoStatus pedidoStatus,int? id)
        {
            Pedido pedido = db.Pedidoes.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                pedidoStatus.PedidoId = pedido.PedidoId;
                pedidoStatus.Usuario = User.Identity.Name;
                pedidoStatus.DataStatus = DateTime.Now;
                pedidoStatus.StatusId = 7;
                db.PedidoStatus.Add(pedidoStatus);
                db.SaveChanges();
                return RedirectToAction("Index","Pedido");
            }

            ViewBag.PedidoId = new SelectList(db.Pedidoes, "PedidoId", "Usuario", pedidoStatus.PedidoId);
            ViewBag.StatusId = new SelectList(db.Status, "id", "NomeStatus", pedidoStatus.StatusId);
            return View(pedidoStatus);
        }
    }
}
