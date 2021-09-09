using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity.Auth;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore06ProjectConfig.Services.Auth.Interfaces;
using DotNetCore06ProjectConfig.Models;

namespace DotNetCore06ProjectConfig.Services.Auth
{
    public class NavbarService : INavbarService
    {
        private readonly ApplicationDbContext _context;

        public NavbarService(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        #region Module
        public async Task<int> SaveNavModule(NavModule module)
        {
            if (module.Id != 0)
            {
                _context.NavModules.Update(module);
                await _context.SaveChangesAsync();
                return module.Id;
            }
            await _context.NavModules.AddAsync(module);
            await _context.SaveChangesAsync();
            return module.Id;
        }
        public async Task<IEnumerable<NavModule>> GetAllNavModule()
        {
            return await _context.NavModules.AsNoTracking().ToListAsync();
        }
        public async Task<NavModule> GetNavModuleById(int id)
        {
            return await _context.NavModules.Where(x=>x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<NavModule> GetNavModuleByName(string name)
        {
            return await _context.NavModules.Where(x=>x.Name == name).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteNavModuleById(int id)
        {
            _context.NavModules.Remove(_context.NavModules.Find(id));
            await _context.SaveChangesAsync();
            return true;
        }

        #endregion

        #region Parent
        public async Task<int> SaveNavParent(NavParent parent)
        {
            if (parent.Id != 0)
            {
                _context.NavParents.Update(parent);
                await _context.SaveChangesAsync();
                return parent.Id;
            }
            await _context.NavParents.AddAsync(parent);
            await _context.SaveChangesAsync();
            return parent.Id;
        }

        public async Task<IEnumerable<NavParent>> GetAllNavParent()
        {
            return await _context.NavParents.Include(x=>x.NavModule).AsNoTracking().ToListAsync();
        }

        public async Task<NavParent> GetNavParentById(int id)
        {
            return await _context.NavParents.Include(x=>x.NavModule).Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<NavParent> GetNavParentByName(int moduleId, string name)
        {
            return await _context.NavParents.Where(x => x.NavModuleId == moduleId && x.Name == name).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteNavParentById(int id)
        {
            _context.NavParents.Remove(_context.NavParents.Find(id));
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Band
        public async Task<int> SaveNavBand(NavBand band)
        {
            if (band.Id != 0)
            {
                _context.NavBands.Update(band);
                await _context.SaveChangesAsync();
                return band.Id;
            }
            await _context.NavBands.AddAsync(band);
            await _context.SaveChangesAsync();
            return band.Id;
        }
        public async Task<IEnumerable<NavBand>> GetAllNavBand()
        {
            return await _context.NavBands.Include(x=>x.NavParent).Include(x=>x.NavParent.NavModule).AsNoTracking().ToListAsync();
        }
        public async Task<NavBand> GetNavBandById(int id)
        {
            return await _context.NavBands.Include(x=>x.NavParent).Include(x => x.NavParent.NavModule).Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<NavBand> GetNavBandByName(int moduleId, int parentId, string name)
        {
            return await _context.NavBands
                .Include(x=>x.NavParent)
                .Where(x => x.NavParent.NavModuleId == moduleId && x.NavParentId == parentId && x.Name == name)
                .AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteNavBandById(int id)
        {
            _context.NavBands.Remove(_context.NavBands.Find(id));
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Item
        public async Task<bool> SaveNavItem(NavItem item)
        {
            if (item.Id != 0)
            {
                _context.NavItems.Update(item);
                await _context.SaveChangesAsync();
                return true;
            }
            await _context.NavItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<NavItem>> GetAllNavItem()
        {
            return await _context.NavItems.Include(x=>x.NavBand).Include(x=>x.NavBand.NavParent).Include(x => x.NavBand.NavParent.NavModule).AsNoTracking().ToListAsync();
        }
        public async Task<NavItem> GetNavItemById(int id)
        {
            return await _context.NavItems.Include(x=>x.NavBand).Include(x => x.NavBand.NavParent).Include(x => x.NavBand.NavParent.NavModule).Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<NavItem> GetNavItemByName(int moduleId, int parentId, int bandId, string name)
        {
            return await _context.NavItems
                .Include(x=>x.NavBand)
                .Include(x => x.NavBand.NavParent)
                .Where(x => x.NavBand.NavParent.NavModuleId == moduleId 
                            && x.NavBand.NavParentId == parentId 
                            && x.NavBandId == bandId 
                            && x.Name == name)
                .AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteNavItemById(int id)
        {
            _context.NavItems.Remove(_context.NavItems.Find(id));
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region User Access Page
        public async Task<bool> SaveUserAccessPage(UserAccessPage pageAccess)
        {
            if (pageAccess.Id != 0)
            {
                _context.UserAccessPages.Update(pageAccess);
                await _context.SaveChangesAsync();
                return true;
            }
            await _context.UserAccessPages.AddAsync(pageAccess);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<UserAccessPage>> GetAllUserAccessPage()
        {
            return await _context.UserAccessPages.Include(x => x.Module).Include(x => x.Parent).Include(x => x.Band).AsNoTracking().ToListAsync();
        }
        public async Task<UserAccessPage> GetUserAccessPageById(int id)
        {
            return await _context.UserAccessPages.Include(x => x.Module).Include(x => x.Parent).Include(x => x.Band).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<bool> DeleteUserAccessPageById(int id)
        {
            _context.UserAccessPages.Remove(_context.UserAccessPages.Find(id));
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Company Access page

        public async Task<int> SaveCompanyAccessPage(CompanyAccessPage page)
        {
            if (page.Id != 0)
            {
                _context.CompanyAccessPages.Update(page);
                await _context.SaveChangesAsync();
                return page.Id;
            }
            await _context.CompanyAccessPages.AddAsync(page);
            await _context.SaveChangesAsync();
            return page.Id;

        }
        public async Task<IEnumerable<CompanyAccessPage>> GetAllCompanyAccessPageByCompanyId(int companyId)
        {
            return await _context.CompanyAccessPages.Where(x => x.CompanyId == companyId).ToListAsync();
        }

        #endregion

        #region Others

        public async Task<IEnumerable<NavParent>> GetAllNavParentByModuleId(int moduleId)
        {
            return await _context.NavParents.Include(x=>x.NavModule).Where(x => x.NavModuleId == moduleId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<NavBand>> GetAllNavBandByParentId(int parentId)
        {
            return await _context.NavBands.Include(x => x.NavParent).Where(x => x.NavParentId == parentId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<NavItem>> GetAllNavItemByBandId(int bandId)
        {
            return await _context.NavItems.Include(x => x.NavBand).Where(x => x.NavBandId == bandId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<UserAccessPageSPModel>> GetAllNavbarsFromSP()
        {
            //return await _context.UserAccessPageSPModels.FromSql($"SP_GetAllNavbars").ToListAsync();
            return new List<UserAccessPageSPModel>();
        }  
        public async Task<IEnumerable<UserAccessPage>> GetAllUserAccessPageByRoleId(string roleId)
        {
            return await _context.UserAccessPages.Where(x => x.roleId == roleId).ToListAsync();
        }
        public async Task<IEnumerable<CompanyAccessPage>> GetAllCompanyAccessPageByCompanyId(string companyId)
        {
            return await _context.CompanyAccessPages.Where(x => x.CompanyId == Convert.ToInt64(companyId)).ToListAsync();
        }
        public async Task<IEnumerable<NavModule>> GetAllUserAccessPageModuleGroupByModuleByRoleId(string roleId)
        {
            List<NavModule> modules = new List<NavModule>();

            if (roleId == "Developer")
            {
                modules = await (from m in _context.NavModules
                            select new NavModule
                            {
                                Id = m.Id,
                                Name = m.Name,
                                Area = m.Area,
                                Controller = m.Controller,
                                Action = m.Action,
                                ImgClass = m.ImgClass,
                                ShortOrder = m.ShortOrder,
                                IsChild = m.IsChild,
                                Status = m.Status,
                                NavParents = _context.NavParents.Where(x=>x.NavModuleId == m.Id).ToList()
                            }).AsNoTracking().ToListAsync();
            }
            else
            {
                //modules = await _context.UserAccessPages.Where(x => x.roleId == roleId && x.moduleId > 0)
                //    .Include(x => x.module).Select(x => x.module).OrderBy(x => x.ShortOrder).ToListAsync();

                //foreach (var item in modules)
                //{
                //    item.navParents = await _context.UserAccessPages.Where(x => x.roleId == roleId && x.parentId > 0)
                //        .Include(x => x.parent).Include(x => x.parent.NavModule)
                //        .Where(x => x.parent.NavModuleId == item.Id)
                //        .Select(x => x.parent).OrderBy(x => x.ShortOrder).ToListAsync();
                //}
            }

            return modules;
        }
        public async Task<IEnumerable<NavBand>> GetAllUserAccessBandGroupByParentByRoleId(string roleId, int parentId)
        {
            List<NavBand> bands = new List<NavBand>();

            if (roleId == "Developer")
            {
                bands = await (from b in _context.NavBands
                                 where b.NavParentId == parentId
                                 select new NavBand
                                 {
                                     Id = b.Id,
                                     Name = b.Name,
                                     Area = b.Area,
                                     Controller = b.Controller,
                                     Action = b.Action,
                                     ImgClass = b.ImgClass,
                                     IsChild = b.IsChild,
                                     ShortOrder = b.ShortOrder,
                                     NavParentId = b.NavParentId,
                                     NavParent = b.NavParent,
                                     NavItems = _context.NavItems.Where(x=>x.NavBandId == b.Id).ToList()
                                 }).OrderBy(x=>x.ShortOrder).ToListAsync();
            }
            else
            {
                //bands = await _context.UserAccessPages.Where(x => x.roleId == roleId && x.bandId > 0)
                //    .Include(x => x.band).Where(x => x.band.NavParentId == parentId).Select(x => x.band).OrderBy(x => x.ShortOrder).ToListAsync();

                //foreach (var item in bands)
                //{
                //    item.navItems = await _context.UserAccessPages.Where(x => x.roleId == roleId && x.navItemId > 0)
                //        .Include(x => x.navItem).Include(x => x.navItem.NavBand)
                //        .Where(x => x.navItem.NavBandId == item.Id)
                //        .Select(x => x.navItem).OrderBy(x => x.ShortOrder).ToListAsync();
                //}
            }
            return bands;
        }

        public async Task<IEnumerable<UserAccessPageSPModel>> GetAllNavbarsWithAssignFlag(string roleId)
        {
            //List<UserAccessPageSPModel> allPages = await _context.UserAccessPageSPModels.FromSql($"SP_GetAllNavbars").ToListAsync();
            //List<CompanyBasedUserAccessPage> assigned = await _context.UserAccessPages.Where(x => x.roleId == roleId).ToListAsync();

            //var data = from P in allPages
            //           join M in assigned on P.moduleId equals M.moduleId into MM
            //           from mm in MM.DefaultIfEmpty()
            //           join p in assigned on P.parentId equals p.parentId into PP
            //           from pp in PP.DefaultIfEmpty()
            //           join B in assigned on P.bandId equals B.bandId into BB
            //           from bb in BB.DefaultIfEmpty()
            //           join I in assigned on P.navItemId equals I.navItemId into II
            //           from ii in II.DefaultIfEmpty()
            //           select new
            //           {

            //           }

            //return await _context.UserAccessPageSPModels.FromSql($"SP_GetAllNavbars").ToListAsync();
            return new List<UserAccessPageSPModel>();
        }

        public async Task<bool> DeleteAllUserAccessPageByRoleId(string roleId)
        {
            //_context.UserAccessPages.RemoveRange(await _context.UserAccessPages.Where(x => x.roleId == roleId).ToListAsync());
            //await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAllMenuRelatedSettings()
        {
            //_context.UserAccessPages.RemoveRange(await _context.UserAccessPages.ToListAsync());
            _context.NavParents.RemoveRange(await _context.NavParents.ToListAsync());
            _context.NavBands.RemoveRange(await _context.NavBands.ToListAsync());
            _context.NavItems.RemoveRange(await _context.NavItems.ToListAsync());
            _context.NavModules.RemoveRange(await _context.NavModules.ToListAsync());
          
            await _context.SaveChangesAsync();

            return true;
        }
        #endregion

    }
}
