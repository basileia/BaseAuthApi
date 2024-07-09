using AutoMapper;
using BaseAuthApp_BAL.Extensions;
using BaseAuthApp_BAL.Models;
using BaseAuthApp_BAL.Services;
using BaseAuthApp_DAL.Contracts;
using BaseAuthApp_DAL.Data;
using BaseAuthApp_DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            var error = new Error("ValidationError", "Model validation failed", errors);
            return new BadRequestObjectResult(error);
        };
    });

var Configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDbConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<ServiceUser, ServiceUser>();
builder.Services.AddTransient<BaseAuthMiddleware>();
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseMiddleware<BaseAuthMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
