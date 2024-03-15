using BlazorApp1;
using BlazorApp1.ServiceInterfaces;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddHttpClient("WebApiClient", (httpClient) =>
{
    httpClient.BaseAddress = new Uri("https://localhost:44321/api/");
});


// add http client service
builder.Services.AddScoped<IHttpClientService, HttpClientService>();
builder.Services.AddScoped<IDataService, DataService>();


await builder.Build().RunAsync();
