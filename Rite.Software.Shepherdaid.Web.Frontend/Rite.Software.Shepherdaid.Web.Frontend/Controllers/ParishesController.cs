using Rite.Software.Shepherdaid.DAL.SecurityEntities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Rite.Software.Shepherdaid.Web.Frontend.Controllers
{
    public class ParishesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Parishes
        public ActionResult Index()
        {
            try
            {
                var parishes = db.Parishes.Include(p => p.Diocese);
                return View(parishes.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Parishes/Create
        public ActionResult Create()
        {
            try
            {
                //var dioceseList = (from x in db.Dioceses
                //                             select new Diocese
                //                             {
                //                                 Id = x.Id,
                //                                 //Name = x.Name + " (" + x.Church.Name + ")",
                //                             }).ToList();

                ViewBag.DioceseId = new SelectList(db.Dioceses, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Parishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DioceseId,Name,Address,Email,Website,Phone1,Phone2")] Parish parish)
        {
            try
            {
                parish.RecordedBy = User.Identity.Name;
                parish.DateRecorded = DateTime.Now;
                parish.LastModifiedBy = User.Identity.Name;
                parish.LastDateModified = DateTime.Now;

                db.Parishes.Add(parish);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.DioceseId = new SelectList(db.Dioceses, "Id", "Name", parish.DioceseId);
            return View(parish);
        }

        // GET: Parishes/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                Parish parish = db.Parishes.Find(id);
                ViewBag.DioceseId = new SelectList(db.Dioceses, "Id", "Name", parish.DioceseId);
                return View(parish);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Parishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DioceseId,Name,Address,Email,Website,Phone1,Phone2,RecordedBy,DateRecorded")] Parish parish)
        {
            try
            {
                parish.LastModifiedBy = User.Identity.Name;
                parish.LastDateModified = DateTime.Now;

                db.Entry(parish).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.DioceseId = new SelectList(db.Dioceses, "Id", "Name", parish.DioceseId);
            return View(parish);
        }

        // GET: Parishes/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                Parish parish = db.Parishes.Find(id);
                db.Parishes.Remove(parish);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
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
