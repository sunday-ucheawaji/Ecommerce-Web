using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<CustomUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<EcommerceWebDbContext>()
            .AddDefaultTokenProviders();

builder.Services.AddDbContext<EcommerceWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceWebConnectionString")));

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
