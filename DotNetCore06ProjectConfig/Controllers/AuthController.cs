using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Services.Auth.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCore06ProjectConfig.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService _userService)
        {
            this._userService = _userService;
        }

        [HttpGet]
        [Route("GetAllUserList")]
        public async Task<IActionResult> GetAllUserList()
        {
            IEnumerable<ApplicationUser> userList = await _userService.GetAllUser();

            return Ok(userList);
        }

    }
}
