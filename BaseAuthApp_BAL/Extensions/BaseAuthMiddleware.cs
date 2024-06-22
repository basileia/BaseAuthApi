using BaseAuthApp_DAL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BaseAuthApp_BAL.Extensions
{
    public class BaseAuthMiddleware : IMiddleware
    {
        private readonly IRepositoryUser _repositoryUser;

        public BaseAuthMiddleware(IRepositoryUser repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Authorization header missing.");
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();
            if (!authHeader.StartsWith("Basic ", System.StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid authorization header.");
                return;
            }

            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialstring.Split(':');
            var username = credentials[0];
            var password = credentials[1];

            //if (!IsAuthorized(username, password))  
            //{
            //    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //    await context.Response.WriteAsync("Invalid username or password.");
            //    return;
            //}

            await next(context);
        }

        
    }
}
