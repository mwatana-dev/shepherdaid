using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;

namespace Rite.Software.Shepherdaid.Web.Frontend.Controllers
{
    public class DioceseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Diocese
        [BreadCrumb(Clear =true, Label ="Dioceses")]
        public ActionResult Index()
        {
            try
            {
                var dioceses = db.Dioceses.Include(d => d.Church);
                return View(dioceses.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Diocese/Create
        [BreadCrumb(Label = "Create Diocese")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.ChurchId = new SelectList(db.Churches, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Diocese/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ChurchId,Name,Address,Email,Website,Phone1,Phone2")] Diocese diocese)
        {
            try
            {
                diocese.RecordedBy = User.Identity.Name;
                diocese.DateRecorded = DateTime.Now;
                diocese.LastModifiedBy = User.Identity.Name;
                diocese.LastDateModified = DateTime.Now;

                db.Dioceses.Add(diocese);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            ViewBag.ChurchId = new SelectList(db.Churches, "Id", "Name", diocese.ChurchId);
            return View(diocese);
        }

        // GET: Diocese/Edit/5
        [BreadCrumb(Label = "Edit Diocese")]
        public ActionResult Edit(int id)
        {

            try
            {
                Diocese diocese = db.Dioceses.Find(id);
                ViewBag.ChurchId = new SelectList(db.Churches, "Id", "Name", diocese.ChurchId);
                return View(diocese);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: Diocese/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ChurchId,Name,Address,Email,Website,Phone1,Phone2,RecordedBy,DateRecorded")] Diocese diocese)
        {
            try
            {
                diocese.LastModifiedBy = User.Identity.Name;
                diocese.LastDateModified = DateTime.Now;

                db.Entry(diocese).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.ChurchId = new SelectList(db.Churches, "Id", "Name", diocese.ChurchId);
            return View(diocese);
        }

        // GET: Diocese/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Diocese diocese = db.Dioceses.Find(id);
                db.Dioceses.Remove(diocese);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
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
