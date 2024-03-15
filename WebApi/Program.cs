using Application;
using Application.Helpers;
using AutoMapper;
using DataAccess;
using Serilog;
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


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//https://www.youtube.com/watch?v=3eFQkchyF2A
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("https://localhost:7124");
    });
});

//data related services
builder.Services.AddApplication();
builder.Services.AddInfractructure(builder.Configuration);


//add automapper
var mapperConfig = new MapperConfiguration(o => o.AddProfile(new AutoMapperProfile()));
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();


//error handling
app.UseMiddleware<ExceptionHandlingMiddleware>();


//Use Serilog
app.UseSerilogRequestLogging();


app.UseAuthorization();

app.MapControllers();

app.Run();
