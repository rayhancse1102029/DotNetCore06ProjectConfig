using DotNetCore06ProjectConfig.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Thana", Schema = "MasterData")]
    public class Thana: Base
    {
        public string? thanaCode { get; set; }

        [Required]
        public string? thanaName { get; set; }
        public string? thanaNameBn { get; set; }

      
        public string? shortName { get; set; }

        public int districtId { get; set; }
        public District? district { get; set; }
    }
}
