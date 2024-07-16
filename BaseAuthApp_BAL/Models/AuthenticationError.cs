using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseAuthApp_BAL.Models
{
    public static class AuthenticationError
    {
        public static readonly Error InvalidCredentials = new("Unauthorized", "Invalid username or password");
        public static readonly Error AuthHeaderMissing = new("Unauthorized", "Authorization header is missing");
        public static readonly Error InvalidAuthHeader = new("Unauthorized", "Invalid authorization header");
        public static readonly Error UnauthorizedAccess = new("Unauthorized", "You don't have access for this content");
    }
}
