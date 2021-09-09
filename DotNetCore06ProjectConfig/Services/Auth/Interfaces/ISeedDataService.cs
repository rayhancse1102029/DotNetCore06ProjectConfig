using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Services.Auth.Interfaces
{
    public interface ISeedDataService
    {
        Task<string> SeedRoles();
        Task<string> SeedUser();
        Task<string> SeedRoleAssignToUserRole();
        Task<string> SeedMasterData();
        Task<string> SeedRolesIntoApsNetCompanyRoles(int companyId);

        //Task<string> SeedMenubar();
    }
}
