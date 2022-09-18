using KratosBlazorApp.Domain.Entities;
using KratosBlazorApp.Server.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KratosBlazorApp.Server.Extentions
{
    public class ApiControllerBase : ControllerBase
    {
        public IUserService _userService;

        public ApiControllerBase(IUserService userService)
        {
            this._userService = userService;
        }

        private User secureUser;
        public User SecureUser
        {
            get
            {
                if (this.HttpContext.User != null && this.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (this.secureUser != null)
                    {
                        return this.secureUser;
                    }
                    else
                    {
                        var ident = (this.HttpContext.User.Identity as System.Security.Claims.ClaimsIdentity).Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        if (ident != null)
                        {
                            this.secureUser = _userService.GeAccountByIdentityId(ident.Value);
                        }
                    }
                }
                return this.secureUser;
            }
        }
    }
}
