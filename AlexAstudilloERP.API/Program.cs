using AlexAstudilloERP.API.Extensions;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.Application.Services.Common;
using AlexAstudilloERP.Application.Services.Custom;
using AlexAstudilloERP.Application.Services.Public;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Common;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using AlexAstudilloERP.Infrastructure.Connections;
using AlexAstudilloERP.Infrastructure.Repositories.Common;
using AlexAstudilloERP.Infrastructure.Repositories.Public;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;

string _swaggerDocName = "v1.0";

var builder = WebApplication.CreateBuilder(args);

//Configure log.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(_swaggerDocName, new OpenApiInfo { Title = "ALEX ASTUDILLO ERP API v1.0", Version = "1.0" });
});

builder.Services.AddDbContext<PostgreSQLContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
});

#region Declare all repositories
// Common schema
builder.Services.AddScoped<IJwtBlacklistRepository, JwtBlacklistRepository>();

// Public schema
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IPersonDocumentTypeRepository, PersonDocumentTypeRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPoliticalDivisionRepository, PoliticalDivisionRepository>();
builder.Services.AddScoped<IPoliticalDivisionTypeRepository, PoliticalDivisionTypeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Declare all services
// Singleton services.
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddSingleton<ISetData, SetData>();
builder.Services.AddSingleton<ITokenService, TokenService>();

// Custom services.
builder.Services.AddScoped<IValidateData, ValidateData>();

// Common schema
builder.Services.AddScoped<IJwtBlacklistService, JwtBlacklistService>();

// Public schema
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IPersonDocumentTypeService, PersonDocumentTypeService>();
builder.Services.AddScoped<IPoliticalDivisionService, PoliticalDivisionService>();
builder.Services.AddScoped<IPoliticalDivisionTypeService, PoliticalDivisionTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

// Enable Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", policyBuilder =>
    {
        // Read cors from json file configuration.
        List<string> cors = new();
        builder.Configuration.GetSection("Cors").Bind(cors);
        cors.ForEach(cor => policyBuilder.WithOrigins(cor).Build());
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.SetIsOriginAllowed(_ => true);
    });
});

// Enable JWT.
builder.Services.AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
        };
        options.Events = new AuthorizeHandler();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/" + _swaggerDocName + "/swagger.json", "Cities V1.0");
    });
}

app.UseCors("EnableCORS");

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
