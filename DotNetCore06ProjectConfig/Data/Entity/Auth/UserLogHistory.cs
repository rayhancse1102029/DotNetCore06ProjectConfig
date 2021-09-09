using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotNetCore06ProjectConfig.Data.Entity.Auth
{
    [Table("UserLogHistory", Schema = "Auth")]
    public class UserLogHistory:Base
    {
        [MaxLength(250)]
        public string? UserId { get; set; }
        [MaxLength(250)]
        public DateTime LogTime { get; set; }
        public int? Status { get; set; }
        [MaxLength(250)]
        public string? IpAddress { get; set; }
        [MaxLength(250)]
        public string? BrowserName { get; set; }
        [MaxLength(250)]
        public string? PcName { get; set; }
        [NotMapped]
        public string? StatusName { get; set; }
    }
}
