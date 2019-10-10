using Rite.Software.Shepherdaid.DAL.SecurityEntities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace Rite.Software.Shepherdaid.Web.Frontend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            Database.SetInitializer(new ApplicationDbContextExtension());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (!AddDefaultRoles())
            {
                return;
            }
        }       


        private bool AddDefaultRoles()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();

                int count = context.AppUserRoles.Where(x => x.AppUser.UserName.Contains("info@shepherdaid.org")).Count();
                if (count > 0)
                {
                    return true;
                }
                //add all the added roles to the super admin group
                int groupID = context.ApplicationGroups.Where(x => x.Name.Contains("Super Admin")).First().Id;

                var roleList = context.AppRoles.ToList();
                List<ApplicationGroupRole> applicationGroupRoleList = new List<ApplicationGroupRole>();
                foreach (var item in roleList)
                {
                    ApplicationGroupRole applicationGroupRole = new ApplicationGroupRole()
                    {
                        AppRoleId = item.Id,
                        ApplicationGroupId = groupID,
                        RecordedBy = "Application",
                        DateRecorded = DateTime.Now,
                        LastModifiedBy = "Application",
                        LastDateModified = DateTime.Now
                    };
                    applicationGroupRoleList.Add(applicationGroupRole);
                }
                context.ApplicationGroupRoles.AddRange(applicationGroupRoleList);
                context.SaveChanges();

                //assign all the roles to super adinm
                string userID = context.AppUsers.Where(x => x.UserName.Contains("info@shepherdaid.org")).First().Id;
                List<AppUserRole> appUserRoleList = new List<AppUserRole>();
                foreach (var item in roleList)
                {
                    AppUserRole appUserRole = new AppUserRole()
                    {
                        RoleId = item.Id,
                        UserId = userID,
                        RecordedBy = "Application",
                        DateRecorded = DateTime.Now,
                        LastModifiedBy = "Application",
                        LastDateModified = DateTime.Now,                        
                    };
                    appUserRoleList.Add(appUserRole);
                }
                context.AppUserRoles.AddRange(appUserRoleList);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
