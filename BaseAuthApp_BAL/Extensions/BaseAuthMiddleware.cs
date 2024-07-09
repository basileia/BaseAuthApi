﻿using BaseAuthApp_BAL.Models;
using BaseAuthApp_BAL.Services;
using BaseAuthApp_DAL.Contracts;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Json;

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
            if (!context.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues value))
            {
                await WriteErrorResponse(context, AuthenticationError.AuthHeaderMissing);
                return;
            }

            var authHeader = value.ToString();
            if (!authHeader.StartsWith("Basic ", System.StringComparison.OrdinalIgnoreCase))
            {
                await WriteErrorResponse(context, AuthenticationError.InvalidAuthHeader);
                return;
            }

            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
            var credentials = credentialstring.Split(':');
            
            if (credentials.Length == 2)
            {
                var username = credentials[0];
                var password = credentials[1];

                if (await _serviceUser.ValidateUserAsync(username, password))
                {                   
                    await next(context);
                    return;
                }                
            }
            await WriteErrorResponse(context, AuthenticationError.InvalidCredentials);
        }

        private static async Task WriteErrorResponse(HttpContext context, Error error)
        {
            var errorJson = JsonSerializer.Serialize(error);

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(errorJson);
        }
    }
}
