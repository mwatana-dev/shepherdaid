using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class MemberDocument
    {
        [Key]
        public long Id { get; set; }
        public long MemberID { get; set; }
        public int DocumentTypeID { get; set; }
        public string DocumentPath { get; set; }
        public string RecordedBy { get; set; }
        public System.DateTime DateRecorded { get; set; }

        public virtual DocumentType DocumentType { get; set; }
        public virtual Member Member { get; set; }
    }
}