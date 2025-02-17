using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using api.Data;
using Microsoft.Extensions.Configuration;
using System;
// identity
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.Interfaces;
using api.Services;
using Microsoft.OpenApi.Models;
// newtonsoft
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

// EMAÝL
builder.Services.Configure<EmailService>(builder.Configuration.GetSection("Email"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add controllers
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "Bearer",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new string[] { }
        }
    });
});

// for sqlite (make sure the connection string is correct in appsettings.json)
var sqliteConnectionString = builder.Configuration.GetConnectionString("MyPortfolio_v3_DB");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(sqliteConnectionString)
);

// Dependency injections
builder.Services.AddSingleton<EmailService>();
builder.Services.AddTransient<IPhotoService, PhotoService>();
builder.Services.AddTransient<IUserInterface, EfUserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEducationCategoryInterface, EfEducationCategoryRepository>();
builder.Services.AddTransient<IEducationInterface, EfEducationRepository>();
builder.Services.AddTransient<IExperienceInterface, EfExperiencesRepository>();
builder.Services.AddTransient<IHobbyInterface, EfHobbyRepository>();
builder.Services.AddTransient<ILanguageInterface, EfLanguageRepository>();
builder.Services.AddTransient<ITechnologyInterface, EfTechnologyRepository>();
builder.Services.AddTransient<IProjectCategoryInterface, EfProjectCategoryRepository>();
builder.Services.AddTransient<IProjectsInterface, EfProjectsRepository>();
builder.Services.AddTransient<ITalentCategoryInterface, EfTalentCategoriesRepository>();
builder.Services.AddTransient<ITalentsInterface, EfTalentsRepository>();
//builder.Services.AddTransient<ISocialMediaPlatformInterface, EfSocialMediaPlatformRepository>();
//builder.Services.AddTransient<ISocialMediaInterface, EfSocialMediaRepository>();


// Identity setup
builder.Services.AddIdentity<AppUsers, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Add in-memory session store
builder.Services.AddDistributedMemoryCache();  // In-memory cache for session

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session süresi
});

// Add Authentication and Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
    app.UseDeveloperExceptionPage();
}

app.UseHsts();

// static files
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseRouting();

// Authentication & Authorization Middleware
app.UseAuthentication();
app.UseAuthorization();

// Enable Session middleware
app.UseSession();

// Enable CORS policy
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

// Test an endpoint
app.MapGet("/hello", () => "Hello World");

app.Run();
