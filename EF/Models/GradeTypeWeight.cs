using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("GRADE_TYPE_WEIGHT")]
    public partial class GradeTypeWeight
    {
        public GradeTypeWeight()
        {
            Grades = new HashSet<Grade>();
        }

        [Key]
        [Column("SCHOOL_ID")]
        public int SchoolId { get; set; }
        [Key]
        [Column("SECTION_ID")]
        public int SectionId { get; set; }
        [Key]
        [Column("GRADE_TYPE_CODE")]
        [StringLength(2)]
        public string GradeTypeCode { get; set; }
        [Column("NUMBER_PER_SECTION")]
        public byte NumberPerSection { get; set; }
        [Column("PERCENT_OF_FINAL_GRADE")]
        public byte PercentOfFinalGrade { get; set; }
        [Column("DROP_LOWEST")]
        public bool DropLowest { get; set; }
        [Required]
        [Column("CREATED_BY")]
        [StringLength(30)]
        public string CreatedBy { get; set; }
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column("MODIFIED_BY")]
        [StringLength(30)]
        public string ModifiedBy { get; set; }
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("SchoolId,GradeTypeCode")]
        [InverseProperty("GradeTypeWeights")]
        public virtual GradeType GradeType { get; set; }
        [ForeignKey("SectionId,SchoolId")]
        [InverseProperty(nameof(Section.GradeTypeWeights))]
        public virtual Section S { get; set; }
        [ForeignKey(nameof(SchoolId))]
        [InverseProperty("GradeTypeWeights")]
        public virtual School School { get; set; }
        [InverseProperty(nameof(Grade.GradeTypeWeight))]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
