using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.MasterData
{
    [Table("Resource", Schema = "MasterData")]
    public class Resource : Base
    {
        [MaxLength(20)]
        public string? nationalID { get; set; }

        [MaxLength(150)]
        public string? nameEnglish { get; set; }
        [MaxLength(150)]
        public string? nameBangla { get; set; }

        public string? LicenseNumber { get; set; }

        public string? eTinNumber { get; set; }

        public string? VATNumber { get; set; }

        [MaxLength(50)]
        public string? nationality { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }
        [MaxLength(50)]
        public string? telephoneResidence { get; set; }
        [MaxLength(50)]
        public string? pabx { get; set; }

        [MaxLength(16)]
        public string? mobileNumberPersonal { get; set; }

        [MaxLength(16)]
        public string? phone { get; set; }
        [MaxLength(15)]
        public string? mobile { get; set; }
        [MaxLength(150)]
        public string? email { get; set; }

        public string? alternativeEmail { get; set; }
    }
}
