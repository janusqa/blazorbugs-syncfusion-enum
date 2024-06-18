using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// SyncFusion
var syncFusionApiKey = "";
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(syncFusionApiKey);
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
