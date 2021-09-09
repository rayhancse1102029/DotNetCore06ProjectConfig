using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Data.Entity.Auth;
using DotNetCore06ProjectConfig.Data.Entity.MasterData;
using DotNetCore06ProjectConfig.Services.Auth.Interfaces;
using DotNetCore06ProjectConfig.Services.MasterData.Interfaces;

namespace DotNetCore06ProjectConfig.Services.Auth
{
    public class SeedDataService : ISeedDataService
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IRoleService _roleService;
        private readonly INavbarService _navbarService;
        private readonly IMasterDataService _masterDataService;

        public SeedDataService(IMasterDataService _masterDataService, INavbarService _navbarService, SignInManager<ApplicationUser> _signInManager, IRoleService _roleService, ApplicationDbContext _context, UserManager<ApplicationUser> _userManager, RoleManager<ApplicationRole> _roleManager)
        {
            this._roleManager = _roleManager;
            this._userManager = _userManager;
            this._context = _context;
            this._roleService = _roleService;
            this._signInManager = _signInManager;
            this._navbarService = _navbarService;
        }

        public async Task<string> SeedRoles()
        {
            string res = "error";
            try
            {
                List<string> rolesList = new List<string>
                {
                    "Developer","SuperAdmin","Admin","SubAdmin","Client","User"
                };
                foreach (var role in rolesList)
                {
                    ApplicationRole roleObj = new ApplicationRole
                    {
                        Name = role,
                        CreatedAt = _roleService.GetDateTimeNow(),
                        IsActive = true,
                        IsDeleted = false,
                        IsVerified = true
                    };
                    await _roleManager.CreateAsync(roleObj);
                    res = "1. Role Creation <span style='color:limegreen'>Success</span>";
                }

            }
            catch (Exception e)
            {
                res = "Error From: Seed Roles <br> Message: " + e.Message.ToString() + "<br>InnerException: " + e.InnerException.ToString();
            }
            return res;
        }

        public async Task<string> SeedUser()
        {
            string res = "error";
            ApplicationUser user = new ApplicationUser
            {
                UserName = "01952363984",
                //Email = model.Email,
                FullName = "Abu Rayhan",
                PhoneNumber = "01952363984",
                //EmployeeCode = await _userService.GenerateEmployeeCode(),
                //genderId = model.genderId,
                ImgUrl = "favicon.ico",
                //Address = model.address,
                IsVerified = true,
                IsDeleted = false,
                IsActive = true,
                //CreatedBy = User.Identity.Name,
                CreatedAt = _roleService.GetDateTimeNow()
            };
            ApplicationUser userIsExists = await _userManager.FindByNameAsync(user.UserName);
            if (userIsExists == null)
            {
                var result = await _userManager.CreateAsync(user, "123456789");
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    res = "2. User Creation <span style='color:limegreen'>Success</span>";
                }
                else
                {
                    res = "Error From: Create user <br>" + result.Errors.ToString();
                }
            }
            else
            {
                res = "2. User Creation <span style='color:limegreen'>Success</span>";
            }
            
            return res;
        }

        public async Task<string> SeedRoleAssignToUserRole()
        {
            string res = "error";
            ApplicationUser user = await _userManager.FindByNameAsync("01952363984");
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, "Developer");
                res = "3. Assign Role To User <span style='color:limegreen'>Success</span>";
            }
            else
            {
                res = "Error From: Assign Roles <br>User Not Exist to Assign Role";
            }
            return res;
        }

        public async Task<string> SeedMasterData()
        {
            string res = "4. Master Data <br><ul>";
           
            List<CompanyType> CompanyTypes = new List<CompanyType>
            {
                new CompanyType{ Name = "Limited", ShortOrder = 1, CreatedAt = DateTime.Now},
                new CompanyType{ Name = "Shared", ShortOrder = 2, CreatedAt = DateTime.Now},
                new CompanyType{ Name = "Proprietor", ShortOrder = 3, CreatedAt = DateTime.Now},
                new CompanyType{ Name = "Others", ShortOrder = 999, CreatedAt = DateTime.Now}
            };
            List<BusinessType> BusinessTypes = new List<BusinessType>
            {
                new BusinessType{ Name = "Group of Company", ShortOrder = 1, CreatedAt = DateTime.Now},
                new BusinessType{ Name = "Pharmacy", ShortOrder = 2, CreatedAt = DateTime.Now},
                new BusinessType{ Name = "Supershop", ShortOrder = 3, CreatedAt = DateTime.Now},
                new BusinessType{ Name = "Others", ShortOrder = 999, CreatedAt = DateTime.Now}
            };
            // company type
            try
            {
                foreach (var item in CompanyTypes)
                {
                    if(await _context.CompanyTypes.Where(x => x.Name == item.Name).FirstOrDefaultAsync() == null) { }
                    {
                        await _context.CompanyTypes.AddAsync(item);
                        await _context.SaveChangesAsync();
                    }
                }
                res += "<li>Company Types [Done]</li>";

            }
            catch (Exception e)
            {
                res += "<li>Company Types [Error]</li>";
            }
            // Bussiness type
            try
            {
                foreach (var item in BusinessTypes)
                {
                    if (await _context.BusinessTypes.Where(x => x.Name == item.Name).FirstOrDefaultAsync() == null) { }
                    {
                        await _context.BusinessTypes.AddAsync(item);
                        await _context.SaveChangesAsync();
                    }
                }
                res += "<li>Business Types [Done]</li>";
            }
            catch (Exception e)
            {
                res += "<li>Business Types [Error]</li>";
            }


            res += "</ul>";
            return res;
        }

        public async Task<string> SeedRolesIntoApsNetCompanyRoles(int companyId)
        {
            string res = "error";
            try
            {
                List<string> rolesList = new List<string>
                {
                    "SuperAdmin","Admin","SubAdmin","Client","User"
                };
                foreach (var role in rolesList)
                {
                    ApplicationRole roleIsExists = await _roleManager.FindByNameAsync(role);

                    if (roleIsExists == null)
                    {
                        ApplicationRole roleObj = new ApplicationRole
                        {
                            Name = role,
                            CreatedAt = _roleService.GetDateTimeNow(),
                            IsActive = true,
                            IsDeleted = false,
                            IsVerified = true
                        };
                        await _roleManager.CreateAsync(roleObj);

                        roleIsExists = await _roleManager.FindByNameAsync(role);
                        if (roleIsExists != null)
                        {
                            AspNetCompanyRoles isExists =
                                await _roleService.GetAspNetCompanyRolesByRoleIdNCompanyId(roleIsExists.Id, companyId);

                            AspNetCompanyRoles obj = new AspNetCompanyRoles
                            {
                                Id = isExists == null ? 0 : isExists.Id,
                                ApplicationRoleId = roleIsExists.Id,
                                CompanyId = companyId
                            };
                            await _roleService.SaveAspNetCompanyRoles(obj);
                        }
                    }
                    else
                    {
                        AspNetCompanyRoles isExists =
                            await _roleService.GetAspNetCompanyRolesByRoleIdNCompanyId(roleIsExists.Id, companyId);

                        AspNetCompanyRoles obj = new AspNetCompanyRoles
                        {
                            Id = isExists == null ? 0 : isExists.Id,
                            ApplicationRoleId = roleIsExists.Id,
                            CompanyId = companyId
                        };
                        await _roleService.SaveAspNetCompanyRoles(obj);
                    }
                }

                res = "done";
            }
            catch (Exception e)
            {
                res = "Somthing is wrong in => InnerException: " + e.InnerException.ToString();
            }
            return res;
        }


    }
}
