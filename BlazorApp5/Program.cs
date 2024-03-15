using BlazorApp5;
using BlazorApp5.ServiceInterfaces;
using BlazorApp5.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped(sp => new HttpClient
{ BaseAddress = new Uri(builder.HostEnvironment.BaseAddress ?? "https://localhost:44321/api/") });


//builder.Services.AddHttpClient("WebApiClient", httpClient =>
//{
//    httpClient.BaseAddress = new Uri("https://localhost:44321/api/");
//});


// add http client service
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IDataService, DataService>();


//builder.Services.AddHttpClient();

await builder.Build().RunAsync();
