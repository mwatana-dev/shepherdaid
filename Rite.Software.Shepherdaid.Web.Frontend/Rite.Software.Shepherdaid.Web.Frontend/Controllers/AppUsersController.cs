using MvcBreadCrumbs;
using Rite.Software.Shepherdaid.Enums;
using Rite.Software.Shepherdaid.Roles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;

namespace Rite.Software.Shepherdaid.Web.Frontend.Controllers
{
    public class AppUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AppUsers
        [BreadCrumb(Clear = true, Label = "Application Users")]
        public ActionResult Index()
        {
            var users = db.AppUsers.Where(x => x.IsActive == true).Include(a => a.ApplicationGroup);
            return View(users.ToList());
        }

        // GET: AppUsers/Create
        [BreadCrumb(Label = "Create Application User")]
        public ActionResult Create()
        {
            ViewBag.ApplicationGroupId = new SelectList(db.ApplicationGroups, "Id", "Name");
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,FirstName,MiddleName,LastName,PhoneNumber,ApplicationGroupId")] AppUser appUser)
        {
            try
            {
                appUser.IsActive = true;
                appUser.UserName = appUser.Email;
                appUser.PasswordHash = Crypto.HashPassword("WillTriple3@church");
                appUser.SecurityStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
                appUser.RecordedBy = User.Identity.Name;
                appUser.DateRecorded = DateTime.Now;
                appUser.LastModifiedBy = User.Identity.Name;
                appUser.LastDateModified = DateTime.Now;

                db.Users.Add(appUser);
                db.SaveChanges();

                var groupRoles = db.ApplicationGroupRoles.Where(x => x.ApplicationGroupId == appUser.ApplicationGroupId).ToList();
                List<AppUserRole> appUserRoles = new List<AppUserRole>();
                foreach (var item in groupRoles)
                {
                    appUserRoles.Add(
                        new AppUserRole
                        {
                            RoleId = item.AppRoleId.ToString(),
                            UserId = appUser.Id,
                            RecordedBy = User.Identity.Name,
                            DateRecorded = DateTime.Now,
                            LastModifiedBy = User.Identity.Name,
                            LastDateModified = DateTime.Now
                        });
                }
                db.AppUserRoles.AddRange(appUserRoles);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ViewBag.ApplicationGroupId = new SelectList(db.ApplicationGroups, "Id", "Name", appUser.ApplicationGroupId);
                return View(appUser);
            }

        }

        // GET: AppUsers/Edit/5
        [BreadCrumb(Label = "Edit Application User")]
        public ActionResult Edit(string id)
        {

            try
            {
                AppUser appUser = db.AppUsers.Find(id);

                ViewBag.ApplicationGroupId = new SelectList(db.ApplicationGroups, "Id", "Name", appUser.ApplicationGroupId);
                return View(appUser);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,FirstName,MiddleName,LastName,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,IsActive,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName,ApplicationGroupId,RecordedBy,DateRecorded")] AppUser appUser)
        {
            try
            {
                appUser.LastModifiedBy = User.Identity.Name;
                appUser.LastDateModified = DateTime.Now;

                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            ViewBag.ApplicationGroupId = new SelectList(db.ApplicationGroups, "Id", "Name", appUser.ApplicationGroupId);
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Disable(string id)
        {
            try
            {
                AppUser appUser = db.AppUsers.Find(id);
                appUser.IsActive = false;
                appUser.LastModifiedBy = User.Identity.Name;
                appUser.LastDateModified = DateTime.Now;

                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Disabled()
        {
            var users = db.AppUsers.Where(x=>x.IsActive==false).Include(a => a.ApplicationGroup);
            return View(users.ToList());
        }
        public ActionResult Enable(string id)
        {
            try
            {
                AppUser appUser = db.AppUsers.Find(id);
                appUser.IsActive = true;
                appUser.LastModifiedBy = User.Identity.Name;
                appUser.LastDateModified = DateTime.Now;
                db.Entry(appUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Disabled");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Disabled");
            }
        }

        [BreadCrumb(Label = "Application User Roles")]
        public ActionResult UserRoles(string id)
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
        public PartialViewResult AvalaibleRolesPartial(string id)
        {
            try
            {
                //get all roles not assigned to this group
                var assignedGroupList = db.AppUserRoles.Where(x => x.UserId == id).Select(s => s.RoleId).ToList();
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
            catch (Exception)
            {

                return PartialView();
            }
        }

        [ChildActionOnly]
        public PartialViewResult AssignedRolesPartial(string id)
        {
            try
            {
                //get all roles not assigned to this user
                var roleList = db.AppUserRoles.Where(x => x.UserId == id).ToList();
                List<AppUserRole> result = new List<AppUserRole>();
                foreach (var item in roleList)
                {
                    item.AppRole = db.AppRoles.Find(item.RoleId);
                    result.Add(item);
                }
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
            catch (Exception)
            {

                return PartialView();
            }
        }

        public ActionResult AddUserRoles(string[] available)
        {
            string id = Convert.ToString(TempData["id"]);


            try
            {
                foreach (var item in available)
                {
                    AppUserRole applicationUserRole = new AppUserRole()
                    {
                        RoleId = item,
                        UserId = id,
                        RecordedBy = User.Identity.Name,
                        DateRecorded = DateTime.Now,
                        LastModifiedBy = User.Identity.Name,
                        LastDateModified = DateTime.Now,
                    };
                    db.AppUserRoles.Add(applicationUserRole);
                    db.SaveChanges();
                }
                return RedirectToAction("UserRoles", new { id = id });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("UserRoles", new { id = id });
            }
        }

        public ActionResult RevokeUserRoles(string[] assigned)
        {
            string id = Convert.ToString(TempData["id"]);

            try
            {
                foreach (var item in assigned)
                {
                    AppUserRole appUserRole = db.AppUserRoles.Where(x => x.RoleId == item && x.UserId == id).FirstOrDefault();
                    if (appUserRole != null)
                    {
                        db.AppUserRoles.Remove(appUserRole);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("UserRoles", new { id = id });
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return RedirectToAction("UserRoles", new { id = id });
            }
        }

        [BreadCrumb(Label = "Transfer User")]
        public ActionResult Transfer(string id)
        {
            try
            {
                TempData["user_id"] = id;
                int applicationGroupId = db.AppUsers.Find(id).ApplicationGroupId;
                var result = db.ApplicationGroups.Where(x=>x.Id != applicationGroupId).ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult TransferUser(int id)
        {
            string userId = Convert.ToString(TempData["user_id"]);
            AppUser appUser = db.AppUsers.Find(userId);
            try
            {
                //get all the roles of the current group
                var groupRoleList = db.ApplicationGroupRoles.Where(x => x.ApplicationGroupId == appUser.ApplicationGroupId).ToList();
                foreach (var item in groupRoleList)
                {
                    var userRole = db.AppUserRoles.Where(x => x.UserId == userId &&
                                                             x.RoleId == item.AppRoleId).FirstOrDefault();
                    if (userRole != null)
                    {
                        db.AppUserRoles.Remove(userRole);
                    }
                }
                
                appUser.ApplicationGroupId = id;
                db.Entry(appUser).State = EntityState.Modified;

                db.SaveChanges();

                //get the new roles and assgn
                groupRoleList = db.ApplicationGroupRoles.Where(x => x.ApplicationGroupId == id).ToList();
                List<AppUserRole> appUserRoleList = new List<AppUserRole>();
                foreach (var item in groupRoleList)
                {

                    AppUserRole appUserRole = new AppUserRole()
                    {
                        UserId = userId,
                        RoleId = item.AppRoleId,
                        RecordedBy = User.Identity.Name,
                        DateRecorded = DateTime.Now,
                        LastModifiedBy = User.Identity.Name,
                        LastDateModified = DateTime.Now,
                    };
                    appUserRoleList.Add(appUserRole);
                }

                db.AppUserRoles.AddRange(appUserRoleList);
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
