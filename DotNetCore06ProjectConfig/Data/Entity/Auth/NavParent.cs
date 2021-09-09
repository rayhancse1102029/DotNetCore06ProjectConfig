using Microsoft.AspNetCore.Identity;
using DotNetCore06ProjectConfig.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.Auth
{
    [Table("NavParent", Schema = "Auth")]
    public class NavParent : Base
    {
        public int? NavModuleId { get; set; }
        public NavModule? NavModule { get; set; }

        [StringLength(150)]
        public string? Name { get; set; }
        public string? Area { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        [StringLength(150)]
        public string? ImgClass { get; set; }
        public int? ShortOrder { get; set; }
        public bool IsChild { get; set; }
        public Status Status { get; set; }

        [NotMapped]
        public int? Group { get; set; }
    }
}
