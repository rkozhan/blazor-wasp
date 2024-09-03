using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorShop.Web.BlazorWasm;
using MyBlazorShop.Libraries.Services.Product;
using MyBlazorShop.Libraries.Services.Storage;
using MyBlazorShop.Libraries.Services.ShoppingCart;
using static System.Net.WebRequestMethods;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClient = new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
builder.Services.AddScoped(sp => httpClient);

using var response = await httpClient.GetAsync("productlisting.json");
using var stream = await response.Content.ReadAsStreamAsync();

builder.Configuration.AddJsonStream(stream);

using var enviromentResponse = await httpClient.GetAsync("productlisting." + builder.HostEnvironment.Environment + ".json");

if (enviromentResponse.IsSuccessStatusCode)
{
    using var environmentStream = await enviromentResponse.Content.ReadAsStreamAsync();

    builder.Configuration.AddJsonStream(environmentStream);
}

builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddLocalization();

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

await builder.Build().RunAsync();
