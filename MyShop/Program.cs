//using MyShop.Models;
using Reposetories;
using Services;
using Entities;
using Microsoft.EntityFrameworkCore;

using NLog.Web;
using MyShop;


var builder = WebApplication.CreateBuilder(args);
//"Server=SRV2\\PUPILS;Database=327725412_webApi;Trusted_Connection=True;TrustServerCertificate=True
// Add services to the container.
//builder.Host.UseNLog();
builder.Services.AddControllers();
builder.Services.AddDbContext<_327725412WebApiContext>(options => options.UseSqlServer("Server=SRV2\\PUPILS;Database=327725412_webApi;Trusted_Connection=True;TrustServerCertificate=True"));
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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
  
}


// Configure the HTTP request pipeline.
app.UseRatingMiddleware();
app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


app.Run();

