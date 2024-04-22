using System.Text;
using System.Text.Json.Serialization;
using Api.Auth.Models;
using Api.Auth.Services;
using Api.Auth.Usecases;
using Api.Auth.Usecases.Interfaces;
using Api.Database.Context;
using Api.Domains.BookCollection.Mapper;
using Api.Domains.BookCollection.Repository;
using Api.Domains.BookCollection.Usecases;
using Api.Domains.BookCollection.Usecases.Interfaces;
using Api.Domains.Books.Mapper;
using Api.Domains.Books.Repository;
using Api.Domains.Books.Usecases;
using Api.Domains.Books.Usecases.Interfaces;
using Api.Domains.Owner.Mapper;
using Api.Domains.Owner.Repository;
using Api.Domains.Owner.Usecases;
using Api.Domains.Owner.Usecases.Interfaces;
using Api.Filters;
using Api.Repositories;
using Api.Repositories.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>( options =>
{
    options.UseNpgsql(connectionString);
}, ServiceLifetime.Scoped);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddControllers(options =>
    {
        options.Filters.Add(typeof(ApiExceptionFilter));
    })
    .AddJsonOptions(options => 
        { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthUsecase, AuthUsecase>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookOwnerRepository, BookOwnerRepository>();
builder.Services.AddScoped<IBookOwnerUsecase, BookOwnerUsecase>();
builder.Services.AddScoped<IBookCaseRepository, BookCaseRepository>();
builder.Services.AddScoped<IBookCaseUsecase, BookCaseUsecase>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookUsecase, BookUsecase>();
builder.Services.AddAutoMapper(typeof(BookOwnerDTOMapperProfile));
builder.Services.AddAutoMapper(typeof(BookCaseDTOMapperProfile));
builder.Services.AddAutoMapper(typeof(BookDTOMapperProfile));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookBendingAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter the bearer token",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

string secretKey = builder.Configuration.GetSection("JWT")["SecretKey"] ??
                   throw new ArgumentException("Invalid secret key!!");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidAudience = builder.Configuration.GetSection("JWT")["ValidAudience"],
            ValidIssuer = builder.Configuration.GetSection("JWT")["ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
