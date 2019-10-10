using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class SacramentRequirement
    {
        [Key]
        public int Id { get; set; }
        public int SacramentId { get; set; }
        public int RequirementTypeId { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }

        public virtual Sacrament Sacrament { get; set; }
        public virtual RequirementType RequirementType { get; set; }
    }
}