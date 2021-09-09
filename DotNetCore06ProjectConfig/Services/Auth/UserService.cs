using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Services.Auth.Interfaces;

namespace DotNetCore06ProjectConfig.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDbContext _context, UserManager<ApplicationUser> _userManager)
        {
            this._context = _context;
            this._userManager = _userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {
            var model = await _context.Users.Include(x=>x.Company).ToListAsync();

            return model;
        }
        public async Task<IEnumerable<ApplicationUser>> GetAllUserByCompanyId(int companyId)
        {
            var model = await _context.Users.Where(x=>x.CompanyId == companyId).Include(x=>x.Company).ToListAsync();

            return model;
        }
        public async Task<ApplicationUser> GetUserInfoByUserEmail(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;

        }
        public async Task<ApplicationUser> GetUserInfo(string userame)
        {
            ApplicationUser user = await _context.Users.Where(x => x.UserName == userame).FirstOrDefaultAsync();
            return user;
        }
        public async Task<bool> UpdateApplicationUser(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> GenerateEmployeeCode()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int maxId = await _context.Users.CountAsync() + 1;

            string getEmpCode = year + "" + month + "" + maxId;

            return getEmpCode;
        }

       

    }
}
