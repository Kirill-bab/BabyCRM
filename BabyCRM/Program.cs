using System.Data;
using System.Data.SqlClient;
using BabyCRM;
using BabyCRM.Extensions;
using BLL.Managers;
using DAL;
using DAL.DbManagers;
using DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IDbManager<Client>, DbManager<Client>>();
builder.Services.AddSingleton<IEntityManager<Client>, ClientManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RunWithExceptionsHandling(app.Logger);
