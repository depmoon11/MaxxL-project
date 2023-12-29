using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using PWS.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Host.UseSerilog();
builder.Services.AddSwaggerGen();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("logs/logs-.txt")
    .CreateLogger();
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = 2028 * 1024 * 1024;
});


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 2028 * 1024 * 1024;
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new System.Globalization.CultureInfo("en"),
        new System.Globalization.CultureInfo("fr"),
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
