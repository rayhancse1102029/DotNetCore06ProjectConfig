using Microsoft.AspNetCore.Identity;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Data.Entity.Auth
{
    [Table("CompanyBasedUserAccessPage", Schema = "Auth")]
    public class CompanyBasedUserAccessPage : Base
    {
        public int? ApplicationRoleId { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public int? ModuleId { get; set; }
        public NavModule Module { get; set; }
        public int? ParentId { get; set; }
        public NavParent Parent { get; set; }
        public int? BandId { get; set; }
        public NavBand Band { get; set; }
        public int? NavItemId { get; set; }
        public NavItem NavItem { get; set; }        

    }
}
