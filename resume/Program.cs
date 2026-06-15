using System.Globalization;
using BlazorApp1;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddSingleton<LocalizationService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

// ➤ ساختن Host برای دسترسی به JSRuntime قبل از اجرای برنامه
var host = builder.Build();

// ➤ خواندن زبان ذخیره‌شده از LocalStorage
var js = host.Services.GetRequiredService<IJSRuntime>();
var savedCulture = await js.InvokeAsync<string>("localStorage.getItem", "culture");

// اگر زبانی ذخیره نشده بود، پیش‌فرض فارسی باشد
var cultureName = !string.IsNullOrEmpty(savedCulture) ? savedCulture : "fa-IR";
var culture = new CultureInfo(cultureName);

// ➤ اعمال زبان به کل برنامه
CultureInfo.CurrentCulture = culture;
CultureInfo.CurrentUICulture = culture;
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

// اجرای برنامه
await host.RunAsync();