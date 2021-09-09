using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Models
{
    public class UserAccessPageSPModel
    {
        //public string roleId { get; set; }
        public int? ModuleId { get; set; }
        public string? Module { get; set; }
        public int? ParentId { get; set; }
        public string? Parent { get; set; }
        public int? BandId { get; set; }
        public string? Band { get; set; }
        public int? NavItemId { get; set; }
        public string? NavItem { get; set; }
        public int? ModuleOrder { get; set; }
        public int? ParentOrder { get; set; }
        public int? BandOrder { get; set; }
        public int? NavItemOrder { get; set; }
        public string? ModuleImg { get; set; }
        public string? ParentImg { get; set; }
        public string? BandImg { get; set; }
        public string? NavItemImg { get; set; }
    }
}
