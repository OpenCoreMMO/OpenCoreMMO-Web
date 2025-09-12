using System.Net;
using MudBlazor.Services;
using NeoServer.Web.Admin;
using OCM.Application.Requests.Validators;
using OCM.IoC;
using OCM.Web.Admin.Components;
using OCM.Infrastructure.Contexts;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var environment = builder.Environment;

// Configure appsettings
builder.Configuration
    .SetBasePath(environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add authentication
builder.Services.AddAuthentication("CustomAuth")
    .AddCookie("CustomAuth", options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/login";
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddServicesApi();
builder.Services.AddAutoMapperProfiles(typeof(Program).Assembly);

builder.Services.AddLogger(configuration);
builder.Services.AddDatabases(configuration);
builder.Services.AddRepositories();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(
        typeof(Program).Assembly,
        typeof(OCM.Application.Requests.Queries.GetAccountsRequest).Assembly,
        typeof(OCM.Application.UseCases.Commands.Account.LoginCommand).Assembly
    );
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardLimit = 2;
    options.KnownProxies.Add(IPAddress.Parse("127.0.0.1"));
    options.KnownProxies.Add(IPAddress.Parse("0.0.0.0"));
    options.ForwardedForHeaderName = "X-Forwarded-For-My-Custom-Header-Name";
});

builder.Services.AddCors();

builder.Services.AddScoped<ProgressBarState>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(8);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NeoContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();