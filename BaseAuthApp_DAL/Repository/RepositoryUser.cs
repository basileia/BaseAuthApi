using BaseAuthApp_DAL.Contracts;
using BaseAuthApp_DAL.Data;
using BaseAuthApp_DAL.Entities;
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
            return await EntityExistsAsync(user => user.Username == username && user.Password == password);
        }

    }
}
