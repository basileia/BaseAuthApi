using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_DAL.Contracts
{
    public interface IRepositoryUser
    {
        Task<bool> UserExistsAsync(string username, string password);
        Task<bool> UserExistsByUsernameAsync(string username);
    }
}
