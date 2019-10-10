using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class MemberSacrament
    {
        [Key]
        public long Id { get; set; }

        [Display(Name = "Member")]
        public long MemberID { get; set; }

        [Required, Display(Name = "Sacrament")]
        public int SacramentID { get; set; }

        [Required, Display(Name = "Administered By")]
        public string AdministeredBy { get; set; }

        [Required, Display(Name = "Date Administered"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime DateAdministered { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }
        public virtual Sacrament Sacrament { get; set; }
        public virtual Member Member { get; set; }
    }
}