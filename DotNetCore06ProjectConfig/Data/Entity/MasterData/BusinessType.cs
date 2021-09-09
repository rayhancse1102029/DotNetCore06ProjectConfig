using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("BusinessType", Schema = "MasterData")]
    public class BusinessType : Base
    {
        public string? Name { get; set; }
        public int? ShortOrder { get; set; }
    }
}
