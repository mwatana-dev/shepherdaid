using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.SecurityEntities
{
    public class ApplicationGroup
    {
        public ApplicationGroup()
        {
            AppUsers = new HashSet<AppUser>();
            ApplicationGroupRoles = new HashSet<ApplicationGroupRole>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Parish")]
        public int ParishId { get; set; }

        [Required, Display(Name = "Group Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, Display(Name = "Can Change")]
        public bool CanChange { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        public virtual Parish Parish { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<ApplicationGroupRole> ApplicationGroupRoles { get; set; }

    }
}