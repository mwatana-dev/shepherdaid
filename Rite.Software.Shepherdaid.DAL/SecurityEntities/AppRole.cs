using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.SecurityEntities
{
    public class AppRole : IdentityRole
    {
        public AppRole()
        {
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
            AppUserRoles = new HashSet<AppUserRole>();
        }

        [Display(Name = "Rank Type")]
        public int RankTypeId { get; set; }

        public ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }
        public ICollection<AppUserRole> AppUserRoles { get; set; }

        public virtual RankType RankType { get; set; }
    }
}