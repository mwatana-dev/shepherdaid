using Rite.Software.Shepherdaid.DAL.RegistrationEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL
{
    public class StatusType
    {
        public StatusType()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Gender"), StringLength(50)]
        public string Name { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Required, Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime LastDateModified { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}