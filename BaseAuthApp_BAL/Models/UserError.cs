using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_BAL.Models
{
    public static class UserError
    {
        public static readonly Error UserExists = new("User.Exists", "Username is already taken");
    }
}
