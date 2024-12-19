using Microsoft.EntityFrameworkCore;
using travel_agency_system.Models;
using Microsoft.AspNetCore.Identity;

namespace travel_agency_system;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // تسجيل Identity
        builder.Services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<DBContext>()
            .AddDefaultTokenProviders();

        // إضافة DbContext
        builder.Services.AddDbContext<DBContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );

        // إضافة باقي الخدمات
        builder.Services.AddControllers();

        // إضافة Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        // ربط الـ Controllers
        app.MapControllers();

        app.Run();
    }
}