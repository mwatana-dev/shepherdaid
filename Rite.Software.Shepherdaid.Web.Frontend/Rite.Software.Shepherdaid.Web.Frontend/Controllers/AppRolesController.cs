using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;

namespace Rite.Software.Shepherdaid.Web.Frontend
{
    public class AppRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AppRoles
        [BreadCrumb(Clear =true, Label ="Application Roles")]
        public ActionResult Index()
        {
            try
            {
                var roles = db.AppRoles.Include(a => a.RankType);
                return View(roles.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        // GET: AppRoles/Create
        [BreadCrumb(Label = "Create Application Role")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.RankTypeId = new SelectList(db.RankTypes, "Id", "Rank");
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: AppRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,RankTypeId")] AppRole appRole)
        {
            try
            {
                appRole.Id = Guid.NewGuid().ToString();
                db.Roles.Add(appRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }

            ViewBag.RankTypeId = new SelectList(db.RankTypes, "Id", "Rank", appRole.RankTypeId);
            return View(appRole);
        }

        // GET: AppRoles/Edit/5

        [BreadCrumb(Label = "Edit Application Role")]
        public ActionResult Edit(string id)
        {
            try
            {
                AppRole appRole = db.AppRoles.Find(id);
                ViewBag.RankTypeId = new SelectList(db.RankTypes, "Id", "Rank", appRole.RankTypeId);
                return View(appRole);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: AppRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RankTypeId")] AppRole appRole)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(appRole).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Invalid model state.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.RankTypeId = new SelectList(db.RankTypes, "Id", "Rank", appRole.RankTypeId);
            return View(appRole);
        }

        // GET: AppRoles/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                AppRole appRole = db.AppRoles.Find(id);
                db.Roles.Remove(appRole);
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
