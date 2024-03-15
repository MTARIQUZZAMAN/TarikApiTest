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


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
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

app.UseCors("AllowAllHeaders");

//error handling
app.UseMiddleware<ExceptionHandlingMiddleware>();


//Use Serilog
app.UseSerilogRequestLogging();


app.UseAuthorization();

app.MapControllers();

app.Run();
