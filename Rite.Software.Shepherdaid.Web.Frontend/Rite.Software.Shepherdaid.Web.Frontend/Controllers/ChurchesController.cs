using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MvcBreadCrumbs;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;

namespace Rite.Software.Shepherdaid.Web.Frontend.Controllers
{
    public class ChurchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Churches
        [BreadCrumb(Clear = true, Label = "Church")]
        public ActionResult Index()
        {
            return View(db.Churches.ToList());
        }

        // GET: Churches/Create

        [BreadCrumb(Label = "Create Church")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Churches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,Email,Website,Phone1,Phone2")] Church church)
        {
            try
            {
                church.RecordedBy = User.Identity.Name;
                church.LastModifiedBy = User.Identity.Name;
                church.DateRecorded = DateTime.Now;
                church.LastDateModified = DateTime.Now;

                db.Churches.Add(church);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(church);
            }
        }

        // GET: Churches/Edit/5

        [BreadCrumb(Label = "Edit Church")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Church church = db.Churches.Find(id);
            if (church == null)
            {
                return HttpNotFound();
            }
            return View(church);
        }

        // POST: Churches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address,Email,Website,Phone1,Phone2,RecordedBy,DateRecorded,LastModifiedBy,LastDateModified")] Church church)
        {
            if (ModelState.IsValid)
            {
                church.LastModifiedBy = User.Identity.Name;
                church.LastDateModified = DateTime.Now;

                db.Entry(church).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(church);
        }

        // GET: Churches/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Church church = db.Churches.Find(id);
                db.Churches.Remove(church);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
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
