using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("DEVICE_CODES")]
    [Index(nameof(DeviceCode1), Name = "IX_DEVICE_CODES_DEVICE_CODE", IsUnique = true)]
    [Index(nameof(Expiration), Name = "IX_DEVICE_CODES_EXPIRATION")]
    public partial class DeviceCode
    {
        [Key]
        [Column("USER_CODE")]
        [StringLength(200)]
        public string UserCode { get; set; }
        [Required]
        [Column("DEVICE_CODE")]
        [StringLength(200)]
        public string DeviceCode1 { get; set; }
        [Column("SUBJECT_ID")]
        [StringLength(200)]
        public string SubjectId { get; set; }
        [Column("SESSION_ID")]
        [StringLength(100)]
        public string SessionId { get; set; }
        [Required]
        [Column("CLIENT_ID")]
        [StringLength(200)]
        public string ClientId { get; set; }
        [Column("DESCRIPTION")]
        [StringLength(200)]
        public string Description { get; set; }
        [Column("CREATION_TIME")]
        public DateTime CreationTime { get; set; }
        [Column("EXPIRATION")]
        public DateTime Expiration { get; set; }
        [Required]
        [Column("DATA", TypeName = "NCLOB")]
        public string Data { get; set; }
    }
}
