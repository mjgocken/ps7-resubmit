using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("GRADE_TYPE")]
    public partial class GradeType
    {
        public GradeType()
        {
            GradeTypeWeights = new HashSet<GradeTypeWeight>();
        }

        [Key]
        [Column("SCHOOL_ID")]
        public int SchoolId { get; set; }
        [Key]
        [Column("GRADE_TYPE_CODE")]
        [StringLength(2)]
        public string GradeTypeCode { get; set; }
        [Required]
        [Column("DESCRIPTION")]
        [StringLength(50)]
        public string Description { get; set; }
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

        [ForeignKey(nameof(SchoolId))]
        [InverseProperty("GradeTypes")]
        public virtual School School { get; set; }
        [InverseProperty(nameof(GradeTypeWeight.GradeType))]
        public virtual ICollection<GradeTypeWeight> GradeTypeWeights { get; set; }
    }
}
