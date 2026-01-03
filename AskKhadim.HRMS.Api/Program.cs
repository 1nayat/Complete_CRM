// Program.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AskKhadim.HRMS.Api.Security;
using AskKhadim.HRMS.Application.Common.Security;
using AskKhadim.HRMS.Application.Employees.Create;
using AskKhadim.HRMS.Application.Employees.Deactivate;
using AskKhadim.HRMS.Application.Employees.Get;
using AskKhadim.HRMS.Application.Employees.List;
using AskKhadim.HRMS.Application.Employees.Update;
using AskKhadim.HRMS.Domain.Repository;
// Adjust these if your concrete types live in different namespaces:
using AskKhadim.HRMS.Infrastructure.Data;
using AskKhadim.HRMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;         // AskKhadimDbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
// using AskKhadim.HRMS.Infrastructure.Security;  // optional handlers/requirements

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

// ---------- Controllers ----------
builder.Services.AddControllers();

// ---------- DbContext ----------
builder.Services.AddDbContext<AskKhadimDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.UseNetTopologySuite()
    )
);

// ---------- DI: Token service + repositories ----------
// =========================
// Core / Auth / Infrastructure
// =========================
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();


// =========================
// Employee Repositories
// =========================
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();


// =========================
// Employee Handlers (WRITE)
// =========================
builder.Services.AddScoped<CreateEmployeeHandler>();
builder.Services.AddScoped<UpdateEmployeeHandler>();
builder.Services.AddScoped<DeactivateEmployeeHandler>();


// =========================
// Employee Handlers (READ)
// =========================
builder.Services.AddScoped<GetEmployeeHandler>();
builder.Services.AddScoped<GetEmployeesHandler>();





// ---------- JWT Authentication ----------
var jwtSection = configuration.GetSection("Jwt");
var jwtKey = jwtSection["Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    // Dev fallback (NOT for production). Prefer env var / KeyVault in production.
    jwtKey = "dev-fallback-secret-change-in-prod";
    builder.Logging.AddConsole();
    var logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger("Startup");
    logger.LogWarning("Jwt:Key is empty in configuration; using dev fallback. Replace with secure key in production.");
}

var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // In dev, if you need to test non-HTTPS, toggle RequireHttpsMetadata accordingly (not recommended in prod)
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSection["Issuer"],
        ValidAudience = jwtSection["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        NameClaimType = JwtRegisteredClaimNames.Sub,
        RoleClaimType = ClaimTypes.Role,
        ClockSkew = TimeSpan.FromSeconds(30)
    };

});

// ---------- Authorization ----------
builder.Services.AddAuthorization();

// ---------- Swagger (with Authorize button) ----------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AskKhadim HRMS API",
        Version = "v1",
        Description = "API for AskKhadim HRMS"
    });

    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Paste the JWT access token here. Example: <token> (no 'Bearer ' prefix required).",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var req = new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, new string[] { } }
    };
    c.AddSecurityRequirement(req);
});

// ---------- Build ----------
var app = builder.Build();

// ---------- Run seeders in scope (safe) ----------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        // Optional: apply pending migrations automatically in dev (uncomment if you want)
        // var db = services.GetRequiredService<AskKhadimDbContext>();
        // db.Database.Migrate();

        // Run role seeding if your project exposes this static helper
        try
        {
            await SeedRoles.EnsureRolesAsync(services);
            logger.LogInformation("SeedRoles completed.");
        }
        catch (Exception exRoles)
        {
            // If SeedRoles not present or fails, log and continue
            logger.LogWarning(exRoles, "SeedRoles failed or not present.");
        }

        // Run SuperAdmin seeder if available
        try
        {
            await AskKhadim.HRMS.Infrastructure.Seed.SeedSuperAdminDirect.EnsureSuperAdminAsync(services);
            logger.LogInformation("SeedSuperAdminDirect completed.");
        }
        catch (Exception exSuper)
        {
            logger.LogWarning(exSuper, "SeedSuperAdminDirect failed or not present.");
        }
    }
    catch (Exception ex)
    {
        // If seeding throws, log & rethrow so startup fails visibly
        var logger2 = services.GetRequiredService<ILogger<Program>>();
        logger2.LogError(ex, "An error occurred while running seeders.");
        throw;
    }
}

// ---------- Middleware pipeline ----------

// Swagger (exposed for dev; in prod you may limit or protect it)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AskKhadim HRMS API v1");
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseHttpsRedirection();

// IMPORTANT: Authentication must come BEFORE Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
