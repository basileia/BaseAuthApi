using BaseAuthApp_DAL.Contracts;
using BaseAuthApp_DAL.Data;
using BaseAuthApp_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BaseAuthApp_DAL.Repository
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        public RepositoryUser(AppDbContext context) : base(context) { }

        public async Task<bool> UserExistsAsync(string username, string password)
        {
            var user = await GetUserByUsernameAsync(username);
            
            if (user == null)
            {
                return false;
            }

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            
        }

        public async Task<bool> UserExistsByUsernameAsync(string username)
        {
            return await EntityExistsAsync(user => user.Username == username);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await GetByPredicateAsync(user => user.Username == username);
        }

    }
}
