using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rite.Software.Shepherdaid.DAL.RegistrationEntities
{
    public class Sacrament
    {
        public Sacrament()
        {
            MemberSacraments = new HashSet<MemberSacrament>();
            SacramentRequirements = new HashSet<SacramentRequirement>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Sacrament")]
        public string Name { get; set; }

        [Required, Display(Name = "Apply Once")]
        public bool AppliedOnce { get; set; }

        [Required, Display(Name = "Not On Same Day")]
        public bool NotOnSameDay { get; set; }
        public string Description { get; set; }
        public string RecordedBy { get; set; }
        public DateTime DateRecorded { get; set; }

        public virtual ICollection<MemberSacrament> MemberSacraments { get; set; }
        public virtual ICollection<SacramentRequirement> SacramentRequirements { get; set; }

    }
}