using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class NationalityType
    {
        public NationalityType()
        {
            Members = new HashSet<Member>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Country"), StringLength(150)]
        public string Country { get; set; }

        [Required, Display(Name = "Nationality"), StringLength(150)]
        public string Nationality { get; set; }

        [Required, Display(Name = "Num Code")]
        public int NumCode { get; set; }

        [Required, Display(Name = "Two Apha Code"), StringLength(50)]
        public string TwoAphaCode { get; set; }
        
        [Required, Display(Name = "Three Apha Code"), StringLength(50)]
        public string ThreeAphaCode { get; set; }

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