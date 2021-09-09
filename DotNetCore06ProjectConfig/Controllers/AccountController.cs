using DotNetCore06ProjectConfig.Data;
using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Models;
using DotNetCore06ProjectConfig.Service.Helper.Interfaces;
using DotNetCore06ProjectConfig.Services.Auth.Interfaces;
using DotNetCore06ProjectConfig.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore06ProjectConfig.Controllers;
[ApiController]
[Route("[controller]")]

public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly RoleManager<ApplicationRole> _roleManager;
        //private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMasterDataService _masterDataService;
        //private readonly IFileSaveService _fileSave;
        private readonly IUserService _userService;
        //private readonly IRoleService _roleService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //RoleManager<ApplicationRole> _roleManager,
            ILogger<AccountController> logger,
            ApplicationDbContext context,
            //IFileSaveService _fileSave,
            IMasterDataService _masterDataService,
            IUserService _userService
            //IRoleService _roleService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
            this._masterDataService = _masterDataService;
            //this._roleManager = _roleManager;
            //this._fileSave = _fileSave;
            this._userService = _userService;
            //this._roleService = _roleService;
        }

        [TempData]
        public string? ErrorMessage { get; set; }

        

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string msg = "";

            ApplicationUser userIsExists = await _userManager.FindByNameAsync(model.Phone);
            if (userIsExists != null)
            {
                msg = "This Phone Number Already Exists please try again!!!";
            }
            else
            {
                var user = new ApplicationUser
                {
                    UserName = model.Phone,
                    //Email = model.Email,
                    FullName = model.FullName,
                    PhoneNumber = model.Phone,
                    EmployeeCode = await _userService.GenerateEmployeeCode(),
                    //genderId = model.genderId,
                    //ImgUrl = baseUrl + "/" + empFileName,
                    //Address = model.address,
                    IsVerified = false,
                    IsDeleted = false,
                    IsActive = false,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");
                    return Ok(msg);
                }
                else
                {
                    msg = "Something is wrong please try Again!!!";
                }

                //string fileName;
                //string empFileName = String.Empty;
                //string message = _fileSave.SaveUserImage(out fileName, model.Img);
                //var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/";

                //if (message == "success")
                //{
                //    empFileName = fileName;
                //    var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;

                //    var user = new ApplicationUser
                //    {
                //        UserName = model.Phone,
                //        //Email = model.Email,
                //        FullName = model.FullName,
                //        PhoneNumber = model.Phone,
                //        EmployeeCode = await _userService.GenerateEmployeeCode(),
                //        //genderId = model.genderId,
                //        ImgUrl = baseUrl + "/" + empFileName,
                //        //Address = model.address,
                //        IsVerified = false,
                //        IsDeleted = false,
                //        IsActive = false,
                //        CreatedBy = "System",
                //        CreatedAt = _roleService.GetDateTimeNow()
                //    };

                //    var result = await _userManager.CreateAsync(user, model.Password);
                //    if (result.Succeeded)
                //    {
                //        //_logger.LogInformation("User created a new account with password.");

                //        //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //        //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
                //        //await _emailSender.SendEmailConfirmationAsync(model.Email, callbackUrl);

                //        HttpContext.Session.SetString("profileImgLink", user.ImgUrl);
                //        HttpContext.Session.SetString("username", user.FullName);

                //        await _signInManager.SignInAsync(user, isPersistent: false);
                //        _logger.LogInformation("User created a new account with password.");

                //        // POS
                //        return Ok(msg);
                //        // Ecommerce
                //        //return RedirectToAction("HybridIndex", "EcommerceWebsite", new { area = "Ecommerce" });
                //    }
                //    else
                //    {
                //        msg = "Something is wrong please try Again!!!";
                //    }
                //}
                //else
                //{
                //    msg = "Error!!! Invalid Image please try Again";
                //}
            }

        //if (ModelState.IsValid)
        //{

        //}
        //else
        //{
        //    msg = model.Img == null ? "Please select your profile image and try Again!!!"
        //        : String.IsNullOrEmpty(model.Phone) ? "Please enter username and try Again!!!"
        //        : String.IsNullOrEmpty(model.FullName) ? "Please enter full name and try Again!!!"
        //        : String.IsNullOrEmpty(model.Password) ? "Please enter password and try Again!!!"
        //        : String.IsNullOrEmpty(model.ConfirmPassword) ? "Please enter confirm password and try Again!!!"
        //        : "Something is wrong please try Again!!!";
        //}
        // If we got this far, something failed, redisplay form
        //ViewBag.LoginMessage = msg;
        return Ok(msg);
            //return RedirectToAction("HybridIndex", "EcommerceWebsite", new {area = "Ecommerce"});
        }

     

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.Phone);

                if (user != null)
                {
                    if(!user.IsActive)
                    {
                        return Ok("inActive Account");
                    }
                    else if (!user.IsVerified)
                    {
                        return Ok("unVerified Account");
                    }
                    else if(!user.IsDeleted)
                    {
                        // This doesn't count login failures towards account lockout
                        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                        var result = await _signInManager.PasswordSignInAsync(model.Phone, model.Password, model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            //HttpContext.Session.SetString("profileImgLink", user.ImgUrl);
                            //HttpContext.Session.SetString("username", user.FullName);
                            //HttpContext.Session.SetString("userId", user.Id);

                            ApplicationUser findApplicationUser = await _userManager.FindByNameAsync(model.Phone);

                            IList<string> getAllRoles = await _userManager.GetRolesAsync(findApplicationUser);
                            // For POS
                            _logger.LogInformation("User logged in.");
                            return RedirectToAction("Index", "Home");

                            // for Ecommerce
                            //if (getAllRoles.Contains("Developer") || getAllRoles.Contains("Admin") || getAllRoles.Contains("SuperAdmin"))
                            //{
                            //    _logger.LogInformation("User logged in.");
                            //    return RedirectToAction("Index", "Home");
                            //}
                            //else
                            //{
                            //    _logger.LogInformation("User logged in.");
                            //    return RedirectToAction("HybridIndex", "EcommerceWebsite", new { area = "Ecommerce" });
                            //}
                        }                       
                        if (result.IsLockedOut)
                        {
                            _logger.LogWarning("User account locked out.");
                            return Ok("User account locked out.");
                        }
                        else
                        {
                            return Ok("Invalid login attempt.");
                        }
                    }
                    else
                    {
                        //ModelState.AddModelError(string.Empty, "Sorry !!! Your Account Are Locked");
                        return Ok("Sorry !!! Your Account Are Locked");
                    }
                }
                else
                {
                    //ModelState.AddModelError(string.Empty, "Incorrect Username And Password");
                    return Ok("Incorrect Username And Password");
                }
            }
            // If we got this far, something failed, redisplay form
            return Ok("Invalid");
        }


        [HttpPost]
        public IActionResult Logout()
        {
            return Ok("Logout Successfully");
        }

        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Logout Successfully");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion
    }
