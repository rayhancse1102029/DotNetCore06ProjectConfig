using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Brand", Schema = "MasterData")]
    public class Brand : Base
    {
        public string? name { get; set; }
        public string? url { get; set; }
        public string? banner_url { get; set; }
    }
}
