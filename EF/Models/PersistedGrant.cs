using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SWARM.EF.Models
{
    [Table("PERSISTED_GRANTS")]
    [Index(nameof(Expiration), Name = "IX_PERSISTED_GRANTS_EXPIRATION")]
    [Index(nameof(SubjectId), nameof(ClientId), nameof(Type), Name = "IX_PERSISTED_GRANTS_SUBJECT_ID_CLIENT_ID_TYPE")]
    [Index(nameof(SubjectId), nameof(SessionId), nameof(Type), Name = "IX_PERSISTED_GRANTS_SUBJECT_ID_SESSION_ID_TYPE")]
    public partial class PersistedGrant
    {
        [Key]
        [Column("KEY")]
        [StringLength(200)]
        public string Key { get; set; }
        [Required]
        [Column("TYPE")]
        [StringLength(50)]
        public string Type { get; set; }
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
        public DateTime? Expiration { get; set; }
        [Column("CONSUMED_TIME")]
        public DateTime? ConsumedTime { get; set; }
        [Required]
        [Column("DATA", TypeName = "NCLOB")]
        public string Data { get; set; }
    }
}
