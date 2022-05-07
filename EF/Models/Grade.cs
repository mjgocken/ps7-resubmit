using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("GRADE")]
    public partial class Grade
    {
        [Key]
        [Column("SCHOOL_ID")]
        public int SchoolId { get; set; }
        [Key]
        [Column("STUDENT_ID")]
        public int StudentId { get; set; }
        [Key]
        [Column("SECTION_ID")]
        public int SectionId { get; set; }
        [Key]
        [Column("GRADE_TYPE_CODE")]
        [StringLength(2)]
        public string GradeTypeCode { get; set; }
        [Key]
        [Column("GRADE_CODE_OCCURRENCE")]
        public byte GradeCodeOccurrence { get; set; }
        [Column("NUMERIC_GRADE", TypeName = "NUMBER(5,2)")]
        public decimal NumericGrade { get; set; }
        [Column("COMMENTS", TypeName = "CLOB")]
        public string Comments { get; set; }
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

        [ForeignKey("SchoolId,SectionId,GradeTypeCode")]
        [InverseProperty("Grades")]
        public virtual GradeTypeWeight GradeTypeWeight { get; set; }
        [ForeignKey("SectionId,StudentId,SchoolId")]
        [InverseProperty(nameof(Enrollment.Grades))]
        public virtual Enrollment S { get; set; }
        [ForeignKey(nameof(SchoolId))]
        [InverseProperty("Grades")]
        public virtual School School { get; set; }
    }
}
