using KratosBlazorApp.Domain.Entities;
using KratosBlazorApp.Server.Extentions;
using KratosBlazorApp.Server.Middleware.Identity;
using KratosBlazorApp.Server.Services;
using KratosBlazorApp.Shared.Models;
using KratosBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KratosBlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult Get()
        {
            return Ok(new CurrentUser
            {
                IsAuthenticated = true,
                UserName = this.SecureUser.UserName,
                Claims = User.Claims
                         .ToDictionary(c => c.Type, c => c.Value)
            });
        }
     
        [Authorize]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest registerRequest)
        {
            var user = _userService.CreateUser(registerRequest);
            return Ok(user);
        }
    }
}
