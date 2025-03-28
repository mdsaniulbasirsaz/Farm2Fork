using Microsoft.EntityFrameworkCore;
using Farm2Fork.Data;
using Farm2Fork.Services.Interfaces;
using Farm2Fork.Repositories.Implementations;
using Farm2Fork.Repositories.Interfaces;
using Farm2Fork.Services.Implementations;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Farm2Fork.Repositories;
using Farm2Fork.Services;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// **1. Add Database Context (Example with PostgreSQL)**
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// **2. Add Services to DI Container**
builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IUserService, UserService>();  // Register IUserService and UserService
builder.Services.AddScoped<IOtpRepository, OtpRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// **3. Add Swagger Documentation**
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Farm2Fork API",
        Version = "v1",
        Description = "API for Farm2Fork - Fresh Food Delivery"
    });
});

// **4. Configure CORS (For Frontend Integration)**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// **5. Add Authentication & Authorization (Optional for Future Use)**
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// **6. Build App**
var app = builder.Build();

// **7. Configure Middleware Pipeline**
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farm2Fork API v1");
    });
}

// **8. Middleware Configuration**
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // Apply CORS Policy
app.UseAuthentication(); // Future-proof authentication
app.UseAuthorization(); // Future-proof authorization
app.MapControllers(); // Map API controllers

// **9. Run Application**
app.Run();
