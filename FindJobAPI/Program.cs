using FindJobAPI.Data;
using Microsoft.EntityFrameworkCore;
using System;
using FindJobAPI.Repository.Interfaces;
using FindJobAPI.Repository.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DB
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")!);
});

// Register Repository
builder.Services.AddScoped<IAdmin_repository, admin_repository>();
builder.Services.AddScoped<IType_Repository, Type_Repository>();
builder.Services.AddScoped<IIndustry_Repository, Industry_Repository>();
builder.Services.AddScoped<IAccount_Repository, Account_Repository>();
builder.Services.AddScoped<ISeeker_Repository, Seeker_Repository>();
builder.Services.AddScoped<IEmployer_Repository, Employer_Repository>();



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

app.Run();
