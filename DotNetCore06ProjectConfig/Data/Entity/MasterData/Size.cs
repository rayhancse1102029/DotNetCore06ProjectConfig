using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Size", Schema = "MasterData")]
    public class Size : Base
    {
        public string? name { get; set; }
    }
}
