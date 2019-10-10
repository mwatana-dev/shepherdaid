using Rite.Software.Shepherdaid.DAL.SecurityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class Member
    {
        public Member()
        {
            MemberDocuments = new HashSet<MemberDocument>();
            MemberSacraments = new HashSet<MemberSacrament>();
            EmergencyContacts = new HashSet<EmergencyContact>();
        }

        [Key]
        public long Id { get; set; }
        [Display(Name = "Member No.")]
        public string MemberNo { get; set; }
        [Display(Name = "Member Type")]
        public int MemberTypeId { get; set; }
        [Required, Display(Name = "Salutation Type")]
        public int SalutationTypeId { get; set; }
        [Required, Display(Name = "Gender")]
        public int GenderTypeId { get; set; }
        [Required, Display(Name = "Date of Birth"), DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required, Display(Name = "Marital Status")]
        public int MaritalStatusTypeId { get; set; }
        [Required, Display(Name = "Resident Address")]
        public string ResidentAddress { get; set; }
        [Display(Name = "Phone 2"), DataType(DataType.PhoneNumber)]
        public string OfficePhone { get; set; }
        [Required, Display(Name = "Nationality")]
        public int NationalityTypeId { get; set; }
        [Display(Name = "User ID")]
        public string AppUserId { get; set; }

        [Display(Name = "County")]
        public int? CountyID { get; set; }
        public string Region { get; set; }
        [Display(Name = "Status")]
        public int StatusTypeID { get; set; }
        public string FilePath { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }
                
        public virtual County County { get; set; }
        public virtual GenderType GenderType { get; set; }
        public virtual AppUser AppUser { get; set; }
        public virtual MemberType MemberType { get; set; }
        public virtual NationalityType NationalityType { get; set; }
        public virtual SalutationType SalutationType { get; set; }
        public virtual StatusType StatusType { get; set; }
        public virtual MaritalStatusType MaritalStatusType { get; set; }

        public virtual ICollection<MemberDocument> MemberDocuments { get; set; }
        public virtual ICollection<MemberSacrament> MemberSacraments { get; set; }
        public virtual ICollection<EmergencyContact> EmergencyContacts { get; set; }
    }
}