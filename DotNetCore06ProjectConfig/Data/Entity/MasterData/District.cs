using DotNetCore06ProjectConfig.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("District", Schema = "MasterData")]
    public class District: Base
    {
        public string? districtCode { get; set; }

        [Required]
        public string? districtName { get; set; }

     
        public string? shortName { get; set; }
        public string? districtNameBn { get; set; }

        public int? divisionId { get; set; }

        public Division? division { get; set; }
    }
}
