using ForceGetCase.DataAccess.Context;
using ForceGetCase.DataAccess.Entity;
using ForceGetCase.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Services.Concrates
{
    public class UserService
    {
        private readonly ForceGetDbContext _context;

        public UserService(ForceGetDbContext context)
        {
           _context = context;
        }

        public async Task CreateUserAsync(string username, string password)
        {
            var hashedPassword = PasswordHelper.HashPassword(password);

            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

    }
}
