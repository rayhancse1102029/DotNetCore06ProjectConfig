using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Company", Schema = "MasterData")]
    public class Company : Base
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Web { get; set; }
        public string? Logo { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonNumber { get; set; }
        public string? ContactPersonEmail { get; set; }
        public string? ContactPersonDesignation { get; set; }
        public string? IncorporateNumber { get; set; }
        public string? Tin { get; set; }
        public string? Bin { get; set; }
        public int? CompanyTypeId { get; set; }
        public CompanyType? CompanyType { get; set; }
        public int? BusinessTypeId { get; set; }
        public BusinessType? BusinessType { get; set; }

        [NotMapped]
        public int? CountUser { get; set; }

    }
}
