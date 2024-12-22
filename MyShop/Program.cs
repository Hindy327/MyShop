//using MyShop.Models;
using Reposetories;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddScoped<IUserReposetory,UserReposetory>();
builder.Services.AddScoped<IProductReposetory,ProductReposetory>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryReposetory, CategoryReposetory>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderReposetory, OrderReposetory>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddDbContext<_327725412WebApiContext>(options => options.UseSqlServer("Server=SRV2\\PUPILS;Database=327725412_webApi;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
