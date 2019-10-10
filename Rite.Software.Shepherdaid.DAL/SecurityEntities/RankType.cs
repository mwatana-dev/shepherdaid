using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.SecurityEntities
{
    public class RankType
    {

        public RankType()
        {
            AppRoles = new HashSet<AppRole>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Rank Level"), StringLength(50)]
        public string Rank { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        public virtual ICollection<AppRole> AppRoles { get; set; }
    }
}