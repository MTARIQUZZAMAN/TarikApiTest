using Application;
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
    app.UseSwaggerUI();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
