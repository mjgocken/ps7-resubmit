using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ZIPCODE")]
    public partial class Zipcode
    {
        public Zipcode()
        {
            Instructors = new HashSet<Instructor>();
        }

        [Key]
        [Column("ZIP")]
        [StringLength(5)]
        public string Zip { get; set; }
        [Column("CITY")]
        [StringLength(25)]
        public string City { get; set; }
        [Column("STATE")]
        [StringLength(2)]
        public string State { get; set; }
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

        [InverseProperty(nameof(Instructor.ZipNavigation))]
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
