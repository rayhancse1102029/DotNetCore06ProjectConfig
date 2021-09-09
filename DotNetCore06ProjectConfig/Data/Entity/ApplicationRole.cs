using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;

namespace DotNetCore06ProjectConfig.Data.Entity
{
    public class ApplicationRole : IdentityRole
    {
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        [MaxLength(120)]
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        [MaxLength(120)]
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? UpdateCount { get; set; }

        [NotMapped]
        public int? CountCompany { get; set; }
        [NotMapped]
        public int? CountUser { get; set; }

    }
}
