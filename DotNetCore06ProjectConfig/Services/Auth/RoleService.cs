using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Services.Auth.Interfaces;
using DotNetCore06ProjectConfig.Models;

namespace DotNetCore06ProjectConfig.Services.Auth
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext _context, RoleManager<ApplicationRole> _roleManager)
        {
            this._roleManager = _roleManager;
            this._context = _context;
        }

        #region Roles
        public async Task<List<ApplicationRole>> GetAllApplicationRole()
        {
            List<ApplicationRole> data = await _roleManager.Roles.Include(x => x.Company)
                .Select(x => new ApplicationRole
                {
                    Id = x.Id,
                    Name = x.Name,
                    NormalizedName = x.NormalizedName,
                    CompanyId = x.CompanyId > 0 ? x.CompanyId : 0,
                    Company = x.Company != null ? x.Company : new Data.Entity.MasterData.Company { Name = "Dev"},
                    CreatedBy = x.CreatedBy == null ? "Dev" : x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy == null ? "Dev" : x.UpdatedBy,
                    CountCompany = _context.AspNetCompanyRoles.Where(y => y.ApplicationRoleId == x.Id).Count() > 0
                        ? _context.AspNetCompanyRoles.Where(y => y.ApplicationRoleId == x.Id).Count()
                        : 0,
                    CountUser = _context.UserRoles.Where(y => y.RoleId == x.Id).Count() > 0 
                                ? _context.UserRoles.Where(y => y.RoleId == x.Id).Count() : 0
                }).AsNoTracking().ToListAsync();
            return data;
        }

        public async Task<List<ApplicationRole>> GetAllApplicationRole2()
        {
            List<ApplicationRole> data = await _context.Roles.ToListAsync();
            List<ApplicationRole> data2 = await (from role in _context.Roles
                                                join aRole in _context.AspNetCompanyRoles on role.Id equals aRole.ApplicationRoleId
                                                select new ApplicationRole
                                                {
                                                    Id = role.Id,
                                                    Name = role.Name,
                                                    NormalizedName = role.NormalizedName,
                                                    CompanyId = role.CompanyId > 0 ? role.CompanyId : aRole.CompanyId,
                                                    Company = role.Company != null ? role.Company : aRole.Company,
                                                    CreatedBy = role.CreatedBy == null ? "Dev" : aRole.CreatedBy,
                                                    CreatedAt = role.CreatedAt,
                                                    UpdatedAt = role.UpdatedAt,
                                                    UpdatedBy = role.UpdatedBy == null ? "Dev" : aRole.UpdatedBy,
                                                    IsVerified = role.IsVerified,

                                                }).AsNoTracking().ToListAsync();


            data.AddRange(data2);

            return data;
        }

        public async Task<int> SaveApplicationRole(ApplicationRole role)
        {
            try
            {
              await  _roleManager.CreateAsync(role);
              return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<List<ApplicationRole>> GetAllRoleByCompnayId(int companyId)
        {
            List<(string, string)> countUser = (List<(string, string)>)from u in _context.Users
                                                      where u.CompanyId == companyId
                                                      join ur in _context.UserRoles on u.Id equals ur.UserId
                                                      select new {ur.RoleId, ur.UserId};

            List<ApplicationRole> data = await (from crole in _context.AspNetCompanyRoles
                where crole.CompanyId == companyId 
                join x in _roleManager.Roles on crole.ApplicationRoleId equals x.Id
                select new ApplicationRole
                {
                    Id = x.Id,
                    Name = x.Name,
                    NormalizedName = x.NormalizedName,
                    CompanyId = x.CompanyId > 0 ? x.CompanyId : 0,
                    Company = x.Company != null ? x.Company : new Data.Entity.MasterData.Company { Name = "Dev" },
                    CreatedBy = x.CreatedBy == null ? "Dev" : x.CreatedBy,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy == null ? "Dev" : x.UpdatedBy,
                    CountUser = countUser.Where(cu=>cu.Item1 == x.Id).Count() > 0
                                ? countUser.Where(cu => cu.Item1 == x.Id).Count() : 0
                }).AsNoTracking().ToListAsync();

            return data;
        }
        public async Task<List<ApplicationRole>> GetAllActiveRoleByCompnayId(int companyId)
        {
            List<ApplicationRole> data = await (from crole in _context.AspNetCompanyRoles
                where crole.CompanyId == companyId && crole.IsDelete < 0
                join r in _roleManager.Roles on crole.ApplicationRoleId equals r.Id
                select r).AsNoTracking().ToListAsync();

            return data;
        }
        public async Task<IdentityRoleViewModel> GetRoleChartData(int companyId)
        {
            IdentityRoleViewModel model = new IdentityRoleViewModel();
            List<string> labels = new List<string>();
            List<string> datas = new List<string>();
            List<string> ids = new List<string>();

            if (companyId > 0)
            {
                var data = await (from u in _context.Users
                    where u.CompanyId == companyId
                    join ur in _context.UserRoles on u.Id equals ur.UserId
                    join r in _context.Roles on ur.RoleId equals r.Id
                    group r by r.Name).AsQueryable().ToListAsync();

                int countId = 1;
                foreach (var item in data)
                {
                    labels.Add(item.Key);
                    int coutData = 0;
                    foreach (var child in item)
                    {
                        coutData++;
                    }
                   datas.Add(Convert.ToString(coutData));
                   ids.Add(Convert.ToString(countId));
                   countId++;
                }

                model.labels = labels;
                model.datas = datas;
                model.ids = ids;

                return model;
            }
            else
            {
                var data = await (from r in _context.Roles
                    join ur in _context.UserRoles on r.Id equals ur.RoleId into ur2
                    from urd in ur2.DefaultIfEmpty()
                    join u in _context.Users  on urd.UserId equals u.Id into u2
                    from ud in u2.DefaultIfEmpty()
                    group r by r.Name).AsQueryable().ToListAsync();

                int countId = 1;
                foreach (var item in data)
                {
                    labels.Add(item.Key);
                    int coutData = 0;
                    foreach (var child in item)
                    {
                        coutData++;
                    }
                    datas.Add(Convert.ToString(coutData));
                    ids.Add(Convert.ToString(countId));
                    countId++;
                }
                model.labels = labels;
                model.datas = datas;
                model.ids = ids;

                return model;
            }
           
        }
        public async Task<string> GetRoleIdByUserId(string userId)
        {
            return await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).FirstOrDefaultAsync();
        }
        #endregion

        #region AspNet Company Role
        public async Task<int> SaveAspNetCompanyRoles(AspNetCompanyRoles role)
        {
            try
            {
                if (role.Id != 0)
                {
                    _context.AspNetCompanyRoles.Update(role);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    await _context.AspNetCompanyRoles.AddAsync(role);
                    await _context.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public async Task<AspNetCompanyRoles> GetAspNetCompanyRolesByRoleIdNCompanyId(string roleId, int companyId)
        {
            return await _context.AspNetCompanyRoles.FirstOrDefaultAsync(x =>
                x.ApplicationRoleId == roleId && x.CompanyId == companyId);
        }


        #endregion

        public DateTime GetDateTimeNow()
        {
            return DateTime.Now.AddHours(-12);
        }
    }
}
