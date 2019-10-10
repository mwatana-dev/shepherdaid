using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MvcBreadCrumbs;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;
using Rite.Software.Shepherdaid.Enums;
using Rite.Software.Shepherdaid.Roles;

namespace Rite.Software.Shepherdaid.Web.Frontend
{
    public class ApplicationGroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationGroups
        [BreadCrumb(Clear = true, Label = "Application Groups")]
        public ActionResult Index()
        {
            try
            {
                var applicationGroups = db.ApplicationGroups.Where(x=>x.CanChange== true).Include(a => a.Parish);
                return View(applicationGroups.ToList());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        // GET: ApplicationGroups/Create
        [BreadCrumb(Label = "Create Application Group")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.ParishId = new SelectList(db.Parishes, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: ApplicationGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParishId,Name")] ApplicationGroup applicationGroup)
        {
            try
            {
                applicationGroup.CanChange = true;
                applicationGroup.RecordedBy = User.Identity.Name;
                applicationGroup.DateRecorded = DateTime.Now;
                applicationGroup.LastModifiedBy = User.Identity.Name;
                applicationGroup.LastDateModified = DateTime.Now;

                db.ApplicationGroups.Add(applicationGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            ViewBag.ParishId = new SelectList(db.Parishes, "Id", "Name", applicationGroup.ParishId);
            return View(applicationGroup);
        }

        // GET: ApplicationGroups/Edit/5
        [BreadCrumb(Label = "Edit Application Group")]
        public ActionResult Edit(int id)
        {

            try
            {
                ApplicationGroup applicationGroup = db.ApplicationGroups.Find(id);
                ViewBag.ParishId = new SelectList(db.Parishes, "Id", "Name", applicationGroup.ParishId);
                return View(applicationGroup);
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: ApplicationGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParishId,Name,CanChange,RecordedBy,DateRecorded")] ApplicationGroup applicationGroup)
        {
            try
            {
                applicationGroup.LastModifiedBy = User.Identity.Name;
                applicationGroup.LastDateModified = DateTime.Now;

                db.Entry(applicationGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.ParishId = new SelectList(db.Parishes, "Id", "Name", applicationGroup.ParishId);
            return View(applicationGroup);
        }

        // GET: ApplicationGroups/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                ApplicationGroup applicationGroup = db.ApplicationGroups.Find(id);
                db.ApplicationGroups.Remove(applicationGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
        }


        [BreadCrumb(Label = "Application Group Roles")]
        public ActionResult GroupRoles(int id)
        {
            try
            {
                TempData["id"] = id;
                return View();
            }
            catch (Exception)
            {

                return View();
            }
        }

        [ChildActionOnly]
        public PartialViewResult AvalaibleRolesPartial(int id)
        {
            try
            {
                //get all roles not assigned to this group
                var assignedGroupList = db.ApplicationGroupRoles.Where(x => x.ApplicationGroupId == id).Select(s => s.AppRoleId).ToList();
                var result = db.AppRoles.ToList();
                result = result.Where(x => !assignedGroupList.Contains(x.Id)).ToList();
                int rankTypeId = 0;
                if (User.IsInRole(MySystemRole.SuperAdmin))
                {
                    //roles remain as retrieved
                }
                else if (User.IsInRole(MySystemRole.ManageChurch))
                {
                    rankTypeId = Convert.ToInt32(MyEnums.RankType.ChurchAdminLevel);
                    result = result.Where(x => x.RankTypeId >= rankTypeId).ToList();
                }
                else if (User.IsInRole(MySystemRole.ManageDiocese))
                {
                    rankTypeId = Convert.ToInt32(MyEnums.RankType.DioceseAdminLevel);
                    result = result.Where(x => x.RankTypeId >= rankTypeId).ToList();
                }
                else if (User.IsInRole(MySystemRole.ManageParishes))
                {
                    rankTypeId = Convert.ToInt32(MyEnums.RankType.ParishAdminLevel);
                    result = result.Where(x => x.RankTypeId >= rankTypeId).ToList();
                }
                else
                {
                    ViewBag.Error = "User does not have rights to see roles";
                }
                return PartialView(result);
            }
            catch (Exception ex)
            {

                return PartialView();
            }
        }

        [ChildActionOnly]
        public PartialViewResult AssignedRolesPartial(int id)
        {
            try
            {
                //get all roles not assigned to this group
                var result = db.ApplicationGroupRoles.Where(x=>x.ApplicationGroupId == id).ToList();
                int rankTypeId = 0;
                if (User.IsInRole(MySystemRole.SuperAdmin))
                {
                    //roles remain as retrieved
                }
                else if (User.IsInRole(MySystemRole.ManageChurch))
                {
                    rankTypeId = Convert.ToInt32(MyEnums.RankType.ChurchAdminLevel);
                    result = result.Where(x => x.AppRole.RankTypeId >= rankTypeId).ToList();
                }
                else if (User.IsInRole(MySystemRole.ManageDiocese))
                {
                    rankTypeId = Convert.ToInt32(MyEnums.RankType.DioceseAdminLevel);
                    result = result.Where(x => x.AppRole.RankTypeId >= rankTypeId).ToList();
                }
                else if (User.IsInRole(MySystemRole.ManageParishes))
                {
                    rankTypeId = Convert.ToInt32(MyEnums.RankType.ParishAdminLevel);
                    result = result.Where(x => x.AppRole.RankTypeId >= rankTypeId).ToList();
                }
                else
                {
                    ViewBag.Error = "User does not have rights to see roles";
                }
                return PartialView(result);
            }
            catch (Exception ex)
            {

                return PartialView();
            }
        }

        public ActionResult AddGroupRoles(string[] available)
        {
            int id = Convert.ToInt32(TempData["id"]);

            try
            {
                foreach (var item in available)
                {
                    ApplicationGroupRole applicationGroupRole = new ApplicationGroupRole()
                    {
                        AppRoleId = item,
                        ApplicationGroupId = id,
                        RecordedBy = User.Identity.Name,
                        DateRecorded = DateTime.Now,
                        LastModifiedBy = User.Identity.Name,
                        LastDateModified = DateTime.Now,
                    };
                    db.ApplicationGroupRoles.Add(applicationGroupRole);
                }

                //add the roles to all group members
                var groupMemberList = db.AppUsers.Where(x => x.ApplicationGroupId == id).ToList();
                foreach (var item in groupMemberList)
                {
                    foreach (var roleId in available)
                    {
                        int cnt = db.AppUserRoles.Where(x => x.UserId == item.Id && x.RoleId == roleId).Count();
                        if (cnt.Equals(0))
                        {
                            AppUserRole appUserRole = new AppUserRole()
                            {
                                RoleId = roleId,
                                UserId = item.Id,
                                RecordedBy = User.Identity.Name,
                                DateRecorded = DateTime.Now,
                                LastModifiedBy = User.Identity.Name,
                                LastDateModified = DateTime.Now,
                            };
                            db.AppUserRoles.Add(appUserRole);
                        }
                    }
                }

                db.SaveChanges();
                return RedirectToAction("GroupRoles", new { id = id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("GroupRoles", new { id = id });
            }
        }

        public ActionResult RevokeGroupRoles(int[] assigned)
        {
            int id = Convert.ToInt32(TempData["id"]);

            try
            {
                //revoke all revoked roles from group users
                var groupUserList = db.AppUsers.Where(x => x.ApplicationGroupId == id).ToList();
                foreach (var item in groupUserList)
                {
                    foreach (var groupRoleId in assigned)
                    {
                        ApplicationGroupRole applicationGroupRole = db.ApplicationGroupRoles.Find(groupRoleId);
                        if (applicationGroupRole != null)
                        {
                            AppUserRole appUserRole = db.AppUserRoles.Where(x => x.RoleId == applicationGroupRole.AppRoleId && x.UserId == item.Id).FirstOrDefault();
                            if (appUserRole != null)
                            {
                                db.AppUserRoles.Remove(appUserRole);
                                //db.SaveChanges();
                            }
                        }
                    }
                }

                foreach (var item in assigned)
                {
                    ApplicationGroupRole applicationGroupRole = db.ApplicationGroupRoles.Find(item);
                    db.ApplicationGroupRoles.Remove(applicationGroupRole);
                }

                
                db.SaveChanges();
                return RedirectToAction("GroupRoles", new { id = id });
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return RedirectToAction("GroupRoles", new { id = id });
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
