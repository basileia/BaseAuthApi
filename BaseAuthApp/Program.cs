using AutoMapper;
using BaseAuthApp_BAL.Extensions;
using BaseAuthApp_BAL.Services;
using BaseAuthApp_DAL.Contracts;
using BaseAuthApp_DAL.Data;
using BaseAuthApp_DAL.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var Configuration = builder.Configuration;
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("WebApiDbConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<ServiceUser, ServiceUser>();
builder.Services.AddScoped<IMapper, Mapper>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<BaseAuthMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
