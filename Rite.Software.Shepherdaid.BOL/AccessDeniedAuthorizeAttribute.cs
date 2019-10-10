using Microsoft.Owin.Security;
using Rite.Software.Shepherdaid.DAL.SecurityEntities;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rite.Software.Shepherdaid.BOL
{
    public class AccessDeniedAuthorizeAttribute : AuthorizeAttribute
    {
        ApplicationDbContext db = new ApplicationDbContext();
        //private bool HasDefaultPasswordChanged(string UserId)
        //{


        //    AppUser appUser = db.AppUsers.Find(UserId);
        //    if (appUser.RecordedBy.Equals(appUser.LastModifiedBy))
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
                //return HttpContext.GetOwinContext().Authentication;
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            if (filterContext.HttpContext.Session["iid"] == null)
            {

                filterContext.HttpContext.Session.Clear();
                filterContext.HttpContext.Session.Abandon();
                AuthenticationManager.SignOut();

                filterContext.Result = new RedirectResult("~/Account/Login");
                return;
            }

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = filterContext.HttpContext.User.Identity.Name;
                AppUser appUser = db.AppUsers.Where(x => x.UserName == userName).FirstOrDefault();

                if (appUser == null)
                {
                    filterContext.Result = new RedirectResult("~/Account/Login");
                    return;
                }
                else
                {
                    if (appUser.RecordedBy.Equals(appUser.LastModifiedBy))//password has not been changed
                    {
                        filterContext.Result = new RedirectResult("~/Manage/Index");
                        return;
                    }
                }
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                //authenticated but does not have access
                filterContext.Result = new RedirectResult("~/Users/Denied");
            }
        }
    }
}