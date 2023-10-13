using AlexAstudilloERP.API.Extensions;
using AlexAstudilloERP.API.Handlers;
using AlexAstudilloERP.API.Mappers;
using AlexAstudilloERP.API.Middlewares;
using AlexAstudilloERP.Application.Services.Common;
using AlexAstudilloERP.Application.Services.Custom;
using AlexAstudilloERP.Application.Services.Public;
using AlexAstudilloERP.Domain.Interfaces.APIs;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Common;
using AlexAstudilloERP.Domain.Interfaces.Repositories.Public;
using AlexAstudilloERP.Domain.Interfaces.Services.Common;
using AlexAstudilloERP.Domain.Interfaces.Services.Custom;
using AlexAstudilloERP.Domain.Interfaces.Services.Public;
using AlexAstudilloERP.Infrastructure.APIs;
using AlexAstudilloERP.Infrastructure.Connections;
using AlexAstudilloERP.Infrastructure.Repositories.Common;
using AlexAstudilloERP.Infrastructure.Repositories.Public;
using AutoMapper;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using NLog;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

string _swaggerDocName = "v1.0";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"alex-astudillo-firebase-adminsdk.json");

//Configure log.
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

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

// Firebase instance to configure in all project.
builder.Services.AddTransient<FirebaseHttpHandler>(sp => new(builder.Configuration["Firebase:APIKey"]!));
builder.Services.AddSingleton(FirebaseApp.Create(new AppOptions()
{
    Credential = GoogleCredential.GetApplicationDefault(),
}));

builder.Services.AddHttpClient<IFirebaseAuthAPI, FirebaseAuthAPI>(httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration["Firebase:BaseURL"]!);
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}).AddHttpMessageHandler<FirebaseHttpHandler>();

#region Declare all repositories
// Common schema
builder.Services.AddScoped<IJwtBlacklistRepository, JwtBlacklistRepository>();

// Public schema
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
builder.Services.AddScoped<IEstablishmentTypeRepository, EstablishmentTypeRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IPersonDocumentTypeRepository, PersonDocumentTypeRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPoliticalDivisionRepository, PoliticalDivisionRepository>();
builder.Services.AddScoped<IPoliticalDivisionTypeRepository, PoliticalDivisionTypeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserMembershipRepository, UserMembershipRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Declare all services
// Singleton services.
builder.Services.AddSingleton(new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
});
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddSingleton<ISetData, SetData>();
builder.Services.AddSingleton<IUtil, Util>();
// Configure mapper.
MapperConfiguration mapper = new(mc =>
{
    mc.AddProfile(new MappingProfile());
});
builder.Services.AddSingleton(mapper.CreateMapper());

// Custom services.
builder.Services.AddScoped<IValidateData, ValidateData>();

// Common schema
builder.Services.AddScoped<IJwtBlacklistService, JwtBlacklistService>();

// Public schema
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IEstablishmentService, EstablishmentService>();
builder.Services.AddScoped<IEstablishmentTypeService, EstablishmentTypeService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IPersonDocumentTypeService, PersonDocumentTypeService>();
builder.Services.AddScoped<IPoliticalDivisionService, PoliticalDivisionService>();
builder.Services.AddScoped<IPoliticalDivisionTypeService, PoliticalDivisionTypeService>();
builder.Services.AddScoped<IRoleService, RoleService>();
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
//builder.Services.AddHttpContextAccessor()
//    .AddAuthorization()
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = builder.Configuration["JWT:Issuer"],
//            ValidAudience = builder.Configuration["JWT:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? "")),
//        };
//        options.Events = new AuthorizeHandler();
//    });

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

// app.UseAuthentication();
// app.UseAuthorization();

app.ConfigureExceptionMiddleware();
app.UseMiddleware<TokenValidationMiddleware>();

app.MapControllers();

app.Run();
