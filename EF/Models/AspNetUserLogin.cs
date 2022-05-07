using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("ASP_NET_USER_LOGINS")]
    [Index(nameof(UserId), Name = "IX_ASP_NET_USER_LOGINS_USER_ID")]
    public partial class AspNetUserLogin
    {
        [Key]
        [Column("LOGIN_PROVIDER")]
        [StringLength(128)]
        public string LoginProvider { get; set; }
        [Key]
        [Column("PROVIDER_KEY")]
        [StringLength(128)]
        public string ProviderKey { get; set; }
        [Column("PROVIDER_DISPLAY_NAME")]
        public string ProviderDisplayName { get; set; }
        [Required]
        [Column("USER_ID")]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(AspNetUser.AspNetUserLogins))]
        public virtual AspNetUser User { get; set; }
    }
}
