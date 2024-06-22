using BaseAuthApp_BAL.Services;
using BaseAuthApp_DAL.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text;

namespace BaseAuthApp_BAL.Extensions

    //pořešit error handler
{
    public class BaseAuthMiddleware : IMiddleware
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly ServiceUser _serviceUser;

        public BaseAuthMiddleware(IRepositoryUser repositoryUser, ServiceUser serviceUser)
        {
            _repositoryUser = repositoryUser;
            _serviceUser = serviceUser;
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

            if (!await _serviceUser.ValidateUserAsync(username, password))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid username or password.");
                return;
            }

            if (await _serviceUser.ValidateUserAsync(username, password))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, "Basic");
                context.User = new ClaimsPrincipal(identity);
            }

            await next(context);
        }
        
    }
}
