using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ASP_NET_USER_TOKENS")]
    public partial class AspNetUserToken
    {
        [Key]
        [Column("USER_ID")]
        public string UserId { get; set; }
        [Key]
        [Column("LOGIN_PROVIDER")]
        [StringLength(128)]
        public string LoginProvider { get; set; }
        [Key]
        [Column("NAME")]
        [StringLength(128)]
        public string Name { get; set; }
        [Column("VALUE")]
        public string Value { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.AspNetUserTokens))]
        public virtual AspNetUser User { get; set; }
    }
}
