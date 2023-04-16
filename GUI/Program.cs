using Blazored.LocalStorage;
using GUI;
using Intervals.Extensions;
using Intervals.Service;
using Intervals.Service.Interface;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddIntervalsHttpClient("https://intervals.icu/api/v1/athlete/", "");
builder.Services.AddSingleton<IReadingMapper, ReadingMapper>();
builder.Services.AddSingleton<IIntervalsAPICommunicator, IntervalsAPICommunicator>();
builder.Services.AddSingleton<IHRVReadingProcessor, HRVReadingProcessor>();
builder.Services.AddBlazoredLocalStorageAsSingleton();

await builder.Build().RunAsync();
