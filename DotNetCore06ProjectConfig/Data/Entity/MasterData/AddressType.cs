using DotNetCore06ProjectConfig.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.Master
{
    [Table("AddressType", Schema = "MasterData")]
    public class AddressType : Base
    {
        [Required]
        public string? typeName { get; set; }
    }
}
