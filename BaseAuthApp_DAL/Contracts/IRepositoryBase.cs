using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_DAL.Contracts
{
    public interface IRepositoryBase<T>
    {
        Task<bool> EntityExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
