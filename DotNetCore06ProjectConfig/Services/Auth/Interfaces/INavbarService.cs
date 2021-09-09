using DotNetCore06ProjectConfig.Data.Entity.Auth;
using DotNetCore06ProjectConfig.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Services.Auth.Interfaces
{
    public interface INavbarService
    {
        #region Module
        Task<int> SaveNavModule(NavModule module);
        Task<IEnumerable<NavModule>> GetAllNavModule();
        Task<NavModule> GetNavModuleById(int id);
        Task<NavModule> GetNavModuleByName(string name);
        Task<bool> DeleteNavModuleById(int id);
        #endregion

        #region Parent
        Task<int> SaveNavParent(NavParent parent);
        Task<IEnumerable<NavParent>> GetAllNavParent();
        Task<NavParent> GetNavParentById(int id);
        Task<NavParent> GetNavParentByName(int moduleId, string name);
        Task<bool> DeleteNavParentById(int id);
        #endregion

        #region Band
        Task<int> SaveNavBand(NavBand band);
        Task<IEnumerable<NavBand>> GetAllNavBand();
        Task<NavBand> GetNavBandById(int id);
        Task<NavBand> GetNavBandByName(int moduleId, int parentId, string name);
        Task<bool> DeleteNavBandById(int id);
        #endregion

        #region Item
        Task<bool> SaveNavItem(NavItem item);
        Task<IEnumerable<NavItem>> GetAllNavItem();
        Task<NavItem> GetNavItemById(int id);
        Task<NavItem> GetNavItemByName(int moduleId, int parentId, int bandId, string name);
        Task<bool> DeleteNavItemById(int id);
        #endregion

        #region User Access Page
        Task<bool> SaveUserAccessPage(UserAccessPage userAccess);
        Task<IEnumerable<UserAccessPage>> GetAllUserAccessPage();
        Task<UserAccessPage> GetUserAccessPageById(int id);
        Task<bool> DeleteUserAccessPageById(int id);
        #endregion

        #region Company Access Page
        Task<int> SaveCompanyAccessPage(CompanyAccessPage page);
        Task<IEnumerable<CompanyAccessPage>> GetAllCompanyAccessPageByCompanyId(int companyId);
        #endregion

        #region Others

        Task<IEnumerable<NavParent>> GetAllNavParentByModuleId(int moduleId);
        Task<IEnumerable<NavBand>> GetAllNavBandByParentId(int parentId);
        Task<IEnumerable<NavItem>> GetAllNavItemByBandId(int bandId);
        Task<IEnumerable<UserAccessPageSPModel>> GetAllNavbarsFromSP();
        Task<IEnumerable<UserAccessPageSPModel>> GetAllNavbarsWithAssignFlag(string roleId);
        Task<IEnumerable<UserAccessPage>> GetAllUserAccessPageByRoleId(string roleId);
        Task<IEnumerable<CompanyAccessPage>> GetAllCompanyAccessPageByCompanyId(string companyId);
        Task<IEnumerable<NavModule>> GetAllUserAccessPageModuleGroupByModuleByRoleId(string roleId);
        Task<IEnumerable<NavBand>> GetAllUserAccessBandGroupByParentByRoleId(string roleId, int parentId);
        Task<bool> DeleteAllUserAccessPageByRoleId(string roleId);
        Task<bool> DeleteAllMenuRelatedSettings();

        #endregion

    }
}
