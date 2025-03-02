using lyulyulyu.Components;
using lyulyulyu.Components.Resources;
using Azure.Monitor.OpenTelemetry.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSingleton<ViewCounter>();
builder.Services.AddHostedService(services => services.GetRequiredService<ViewCounter>());
builder.Services.AddScoped<IChaiManager, ChaiManager>();
builder.Services.AddScoped<ICityDoodleManager, CityDoodleManager>();

// App Insights
var connectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
builder.Services.AddOpenTelemetry().UseAzureMonitor((options =>
{
    options.ConnectionString = connectionString; // Use the connection string
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
