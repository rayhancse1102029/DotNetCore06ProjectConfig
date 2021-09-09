using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;

namespace DotNetCore06ProjectConfig.Data.Entity
{
    public class AspNetCompanyRoles : Base
    {
        public string? ApplicationRoleId { get; set; }
        public ApplicationRole? ApplicationRole { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; }

    }
}
