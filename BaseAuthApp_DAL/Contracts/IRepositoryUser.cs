using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_DAL.Contracts
{
    public interface IRepositoryUser
    {
        Task<bool> IsAuthorized(string username, string password);
    }
}
