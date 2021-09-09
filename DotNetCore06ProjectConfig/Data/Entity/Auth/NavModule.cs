using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DotNetCore06ProjectConfig.Data.Enum;

namespace DotNetCore06ProjectConfig.Data.Entity.Auth
{
    [Table("NavModule", Schema = "Auth")]
    public class NavModule:Base
    {
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
        [NotMapped]
        public IEnumerable<NavParent>? NavParents { get; set; }
    }
}
