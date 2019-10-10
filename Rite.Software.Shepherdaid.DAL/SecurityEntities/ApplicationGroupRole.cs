using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rite.Software.Shepherdaid.DAL.SecurityEntities
{
    public class ApplicationGroupRole
    {
        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Application Group"), Index("IX_GroupAndRole", 1, IsUnique = true)]
        public int ApplicationGroupId { get; set; }

        [Required, Display(Name = "Application Role"), Index("IX_GroupAndRole", 2, IsUnique = true), StringLength(128)]
        public string AppRoleId { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LastDateModified { get; set; }

        public virtual ApplicationGroup ApplicationGroup { get; set; }
        public virtual AppRole AppRole { get; set; }

    }
}