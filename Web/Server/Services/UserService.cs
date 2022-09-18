using KratosBlazorApp.Domain.Contexts;
using KratosBlazorApp.Domain.Entities;
using KratosBlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Ory.Kratos.Client;

namespace KratosBlazorApp.Server.Services
{
    public interface IUserService
    {
        User CreateUser(RegisterRequest registerRequest);
        User? GeAccountByIdentityId(string identityId);
        User? GetUserByUsername(string userName);
    }

    public class UserService : IUserService
    {
        private readonly KratosBlazorAppContext _dbContext;
        public UserService(KratosBlazorAppContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public User CreateUser(RegisterRequest registerRequest)
        {
            var user = new User { 
                IdentityId = registerRequest.IdentityId, 
                FirstName = registerRequest.Traits.Name.First,
                LastName = registerRequest.Traits.Name.Last,
                UserName = registerRequest.Traits.Email
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user;
        }
        public User? GeAccountByIdentityId(string identityId)
        {
            return _dbContext.Users.FirstOrDefault(x => x.IdentityId == identityId);
        }
        public User? GetUserByUsername(string userName)
        {
            return _dbContext.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
