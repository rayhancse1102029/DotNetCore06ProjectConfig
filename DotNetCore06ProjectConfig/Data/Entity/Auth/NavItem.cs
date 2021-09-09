using Microsoft.AspNetCore.Identity;
using DotNetCore06ProjectConfig.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.Auth
{
    [Table("NavItem", Schema = "Auth")]
    public class NavItem:Base
    {
        public int? NavBandId { get; set; }
        public NavBand? NavBand { get; set; }

        [StringLength(150)]
        public string? Name { get; set; }
        public string? Area { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        [StringLength(150)]
        public string? ImgClass { get; set; }
        public int? ShortOrder { get; set; }
        public Status Status { get; set; }

        [NotMapped]
        public int? Group { get; set; }
        [NotMapped]
        public string? ModuleName { get; set; }
        [NotMapped]
        public string? ParentName { get; set; }
        [NotMapped]
        public string? BandName { get; set; }
    }
}
