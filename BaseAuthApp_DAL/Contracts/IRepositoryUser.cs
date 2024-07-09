using BaseAuthApp_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_DAL.Contracts
{
    public interface IRepositoryUser : IRepositoryBase<User>
    {
        Task<bool> UserExistsByUsernameAsync(string username);
        Task<User> GetUserByUsernameAsync(string username); 
    }
}
