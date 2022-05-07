using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ASP_NET_ROLE_CLAIMS")]
    [Index(nameof(RoleId), Name = "IX_ASP_NET_ROLE_CLAIMS_ROLE_ID")]
    public partial class AspNetRoleClaim
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("ROLE_ID")]
        public string RoleId { get; set; }
        [Column("CLAIM_TYPE")]
        public string ClaimType { get; set; }
        [Column("CLAIM_VALUE")]
        public string ClaimValue { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(AspNetRole.AspNetRoleClaims))]
        public virtual AspNetRole Role { get; set; }
    }
}
