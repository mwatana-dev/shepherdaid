﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rite.Software.Shepherdaid.DAL.SecurityEntities
{
    public class Diocese
    {
        public Diocese()
        {
            Parishes = new HashSet<Parish>();
        }

        [Key]
        public int Id { get; set; }

        [Required, Display(Name = "Chruch")]
        public int ChurchId { get; set; }

        [Required, Display(Name = "Diocese Name"), StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(250)]
        public string Address { get; set; }

        [Required, DataType(DataType.EmailAddress), StringLength(100)]
        public string Email { get; set; }

        [DataType(DataType.Url), StringLength(75)]
        public string Website { get; set; }


        [Required, Display(Name = "Phone #1"), StringLength(15)]
        public string Phone1 { get; set; }


        [Display(Name = "Phone #2"), StringLength(15)]
        public string Phone2 { get; set; }

        [Required, Display(Name = "Recorded By"), StringLength(50)]
        public string RecordedBy { get; set; }

        [Required, Display(Name = "Date Recorded"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateRecorded { get; set; }

        [Required, Display(Name = "Last Modified By"), StringLength(50)]
        public string LastModifiedBy { get; set; }

        [Display(Name = "Last Date Modified"), DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LastDateModified { get; set; }

        public virtual ICollection<Parish> Parishes { get; set; }

        public virtual Church Church { get; set; }
    }
}