using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ASP_NET_ROLES")]
    [Index(nameof(NormalizedName), Name = "ROLENAMEINDEX", IsUnique = true)]
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
        }

        [Key]
        [Column("ID")]
        public string Id { get; set; }
        [Column("NAME")]
        [StringLength(256)]
        public string Name { get; set; }
        [Column("NORMALIZED_NAME")]
        [StringLength(256)]
        public string NormalizedName { get; set; }
        [Column("CONCURRENCY_STAMP")]
        public string ConcurrencyStamp { get; set; }

        [InverseProperty(nameof(AspNetRoleClaim.Role))]
        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        [InverseProperty(nameof(AspNetUserRole.Role))]
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
    }
}
