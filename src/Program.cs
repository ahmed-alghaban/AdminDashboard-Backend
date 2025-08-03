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
using AdminDashboard.src.Configs.Middleware;
using System.Text.Json.Serialization;

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

builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IUserService, UserService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IRoleService, RoleService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IInventoryService, InventoryService>()
                .AddScoped<ISettingService, SettingService>()
                .AddScoped<IAnalyticsService, AnalyticsService>()
                .AddScoped<IAuditLogService, AuditLogService>()
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
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        RoleClaimType = ClaimTypes.Role,
        ClockSkew = TimeSpan.Zero
    };
    
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
            Console.WriteLine($"Exception details: {context.Exception}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully");
            var claims = context.Principal?.Claims?.Select(c => $"{c.Type}: {c.Value}");
            Console.WriteLine($"Claims: {string.Join(", ", claims ?? new string[0])}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine($"Authorization challenge: {context.Error}, {context.ErrorDescription}");
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            Console.WriteLine("Authorization forbidden - user doesn't have required role");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuditMiddleware>();
app.MapControllers();
app.Run();
