using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("CompanyType", Schema = "MasterData")]
    public class CompanyType : Base
    {
        public string? Name { get; set; }
        public int? ShortOrder { get; set; }
    }
}
