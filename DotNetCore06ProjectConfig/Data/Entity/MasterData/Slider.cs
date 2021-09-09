using DotNetCore06ProjectConfig.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Slider", Schema = "MasterData")]
    public class Slider : Base
    {
        [MaxLength(200)]
        public string? name { get; set; }
        public string? url { get; set; }
        public bool isActive { get; set; }
        public string? titleOne { get; set; }
        public string? titleTwo { get; set; }

        // constractor

        public Slider()
        {
            isActive = false;
        }
    }
}
