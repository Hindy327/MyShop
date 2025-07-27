//using MyShop.Models;
using Reposetories;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using NLog.Web;
using MyShop;
using System;


var builder = WebApplication.CreateBuilder(args);
string connectionString;
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Redis container address
    options.InstanceName = "MyShop_";
});


//"Server=SRV2\\PUPILS;Database=327725412_webApi;Trusted_Connection=True;TrustServerCertificate=True
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<_327725412WebApiContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("School")));
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserReposetory,UserReposetory>();
builder.Services.AddScoped<IProductReposetory,ProductReposetory>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryReposetory, CategoryReposetory>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderReposetory, OrderReposetory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Host.UseNLog();
builder.Services.AddMemoryCache();

// JWT Authentication configuration
var jwtKey = builder.Configuration["Jwt:Key"] ?? "A7f!9s@2kL#8zQw$1pXcVbNmRtYhUjIo";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "MyShopIssuer";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();

//}


// Configure the HTTP request pipeline.
// Add CSP headers middleware
app.Use(async (context, next) =>
{
   context.Response.Headers.Append("Content-Security-Policy-Report-Only",
       "default-src 'self'; " +
       "script-src 'self'; " +
       "style-src 'self'; " +
       "img-src 'self'; " +
       "connect-src 'self'; " +
       "frame-src 'self'; " +
       "object-src 'none';");
   await next();
});


app.UseRatingMiddleware();
app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();

