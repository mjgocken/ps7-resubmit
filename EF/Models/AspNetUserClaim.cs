using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ASP_NET_USER_CLAIMS")]
    [Index(nameof(UserId), Name = "IX_ASP_NET_USER_CLAIMS_USER_ID")]
    public partial class AspNetUserClaim
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("USER_ID")]
        public string UserId { get; set; }
        [Column("CLAIM_TYPE")]
        public string ClaimType { get; set; }
        [Column("CLAIM_VALUE")]
        public string ClaimValue { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.AspNetUserClaims))]
        public virtual AspNetUser User { get; set; }
    }
}
