using DotNetCore06ProjectConfig.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Country", Schema = "MasterData")]
    public class Country : Base
    {
        public string? countryCode { get; set; }

        [Required]
        public string? countryName { get; set; }
        public string? countryNameBn { get; set; }

      
        public string? shortName { get; set; }

    }
}
