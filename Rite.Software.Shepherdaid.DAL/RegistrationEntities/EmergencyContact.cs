using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class EmergencyContact
    {
        [Key]
        public long Id { get; set; }

        [Required, Display(Name = "Salutation")]
        public int SalutationTypeID { get; set; }

        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required, Display(Name = "Member")]
        public long MemberID { get; set; }

        [Required, Display(Name = "Phone 1"), DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }

        [Required, Display(Name = "Residence Address")]
        public string ResidentAddress { get; set; }

        [Display(Name = "Phone 2"), DataType(DataType.PhoneNumber)]
        public string OfficePhone { get; set; }

        [Required, Display(Name = "Relationship")]
        public int RelationshipTypeID { get; set; }

        [Display(Name = "Email Address"), DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }

        public virtual Member Member { get; set; }
        //public virtual SalutationType SalutationType { get; set; }
        public virtual RelationshipType RelationshipType { get; set; }
    }
}