using Application;
using Application.AppSettings;
using Application.Authentication;
using Application.DTOs;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
.ReadFrom.Configuration(builder.Configuration)
.CreateLogger();

//Add Logger
Log.Logger = logger;
builder.Host.UseSerilog(logger);

//connect to sql db
var connectionString = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

//clear default claim type mapping
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


//get JWT 
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);


//add identity config
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Lockout.MaxFailedAccessAttempts = 10;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<AppDbContext>();

//add authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.SaveToken = true;
        jwtOptions.ClaimsIssuer = jwtSettings["Issuer"];
        jwtOptions.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SingningKey"])),
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
        };

        jwtOptions.Events = new JwtBearerEvents

        {
            OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                context.Response.Headers.Add("IS_TOKEN_EXPIRED", "Y");
            return Task.CompletedTask;
        }

        };
    });


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authentication",
        Type = SecuritySchemeType.Http,
        Description = "Enter key",
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new string[] {}
        }
    });
});


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("BlazorApp",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorApp",
        builder =>
        {
            builder.WithOrigins("https://localhost:7124")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
});



//data related services
builder.Services.AddApplication();
builder.Services.AddInfractructure(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseCors("BlazorApp");


if (!app.Environment.IsDevelopment())
{
    //error handling
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    //Use Serilog
    app.UseSerilogRequestLogging();
    app.UseStatusCodePagesWithReExecute("/Errors/Exception/{0}");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
