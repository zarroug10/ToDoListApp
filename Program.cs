using System.Reflection;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using ToDoListApp.Data.Repositories;
using ToDoListApp.Data;
using ToDoListApp.Extensions;
using ToDoListApp.Interface;
using ToDoListApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services and repositories  
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


builder.Services.AddDbContext<DataContext>(opt =>
   opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
       sql => sql.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null))
       .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

builder.Services.AddApplicationServices();

// Configure Identity services for authentication and authorization  
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireDigit = false;
    opt.Password.RequireUppercase = false;
})
   .AddEntityFrameworkStores<DataContext>()
   .AddDefaultTokenProviders();

// Configure CORS  
builder.Services.AddCors(options =>
{
    options.AddPolicy("SecurePolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .WithExposedHeaders("Authorization")
              .SetIsOriginAllowedToAllowWildcardSubdomains()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("SecurePolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
