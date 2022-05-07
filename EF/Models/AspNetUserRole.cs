using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ASP_NET_USER_ROLES")]
    [Index(nameof(RoleId), Name = "IX_ASP_NET_USER_ROLES_ROLE_ID")]
    public partial class AspNetUserRole
    {
        [Key]
        [Column("USER_ID")]
        public string UserId { get; set; }
        [Key]
        [Column("ROLE_ID")]
        public string RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(AspNetRole.AspNetUserRoles))]
        public virtual AspNetRole Role { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.AspNetUserRoles))]
        public virtual AspNetUser User { get; set; }
    }
}
