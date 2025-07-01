using Microsoft.EntityFrameworkCore;
using AdminDashboard.src.Configs;
using AdminDashboard.src.Services;
using AdminDashboard.src.Abstraction;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using AdminDashboard.src.Utilities;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
var defaultConnection = Environment.GetEnvironmentVariable("DB__CONNECTION")
?? throw new InvalidOperationException("DB Connection Does Not Exist");
var jwtKey = Environment.GetEnvironmentVariable("JWT__KEY")
?? throw new InvalidOperationException("JWT Key is missing in environment variables.");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER")
?? throw new InvalidOperationException("JWT Issuer is missing in environment variables.");
var jwtAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")
?? throw new InvalidOperationException("JWT Audience is missing in environment variables.");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IUserService, UserService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<GenerateToken>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(defaultConnection));

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        RoleClaimType = ClaimTypes.Role
    };
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
