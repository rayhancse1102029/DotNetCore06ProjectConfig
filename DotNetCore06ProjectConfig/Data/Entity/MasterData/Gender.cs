using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore06ProjectConfig.Data.Entity;

namespace DotNetCore06ProjectConfig.Models.MasterData
{
    [Table("Gender", Schema = "MasterData")]
    public class Gender : Base
    {
        [DisplayName("Gender")]
        [MaxLength(20)]
        public string? name { get; set; }
    }
}
