using EcommerceWeb.Data;
using EcommerceWeb.Mappings;
using EcommerceWeb.Middlewares;
using EcommerceWeb.Models.Domain;
using EcommerceWeb.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/ecom.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {

        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            },
            Scheme = "Oauth2",
            Name = JwtBearerDefaults.AuthenticationScheme,
            In = ParameterLocation.Header
        },
        new List<string>()
        }
    });
    // This config helps to ensure that the build doesn't fail due to models having the same name
    options.CustomSchemaIds(type => type.ToString());
} );

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddIdentityCore<CustomUser>()
    .AddRoles<IdentityRole<Guid>>()
    .AddTokenProvider<DataProtectorTokenProvider<CustomUser>>("EcommerceWeb")
    .AddEntityFrameworkStores<EcommerceWebDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddDbContext<EcommerceWebDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceWebConnectionString")));



// Dependency Injections
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<ICustomerRepository, SQLCustomerRepository>();
builder.Services.AddScoped<ISupplierRepository, SQLSupplierRepository>();
builder.Services.AddScoped<IStaffRepository, SQLStaffRepository>();
builder.Services.AddScoped<IPromotionRepository, SQLPromotionRepository>();
builder.Services.AddScoped<IProductRepository, SQLProductRepository>();
builder.Services.AddScoped<IImageRepository, LocalImageRepository>();
builder.Services.AddScoped<ICartRepository, SQLCartRepository>();
builder.Services.AddScoped<ICartItemRepository, SQLCartItemRepository>();
builder.Services.AddScoped<IOrderRepository, SQLOrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, SQLOrderDetailRepository>();
builder.Services.AddScoped<IAddressRepository, SQLAddressRepository>();
builder.Services.AddScoped<IBillBoardRepository, SQLBillBoardRepository>();
builder.Services.AddScoped<ICategoryRepository, SQLCategoryRepository>();
builder.Services.AddScoped<IReviewRepository, SQLReviewRepository>();




builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

// JWT configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.MapControllers();

app.Run();
