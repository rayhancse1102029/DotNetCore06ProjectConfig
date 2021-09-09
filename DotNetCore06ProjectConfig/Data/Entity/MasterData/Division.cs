using DotNetCore06ProjectConfig.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Division", Schema = "MasterData")]
    public class Division : Base
    {
        public string? divisionCode { get; set; }

        [Required]
        public string? divisionName { get; set; }
        public string? divisionNameBn { get; set; }

    
        public string? shortName { get; set; }

        public int? countryId { get; set; }

        public Country? country { get; set; }
    }
}
