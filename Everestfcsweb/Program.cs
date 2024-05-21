using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddRazorPages().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddDataProtection();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
{
    x.AccessDeniedPath = "/Account/AccessDenied/";
    x.LoginPath = "/Account/Signinuser/";
});
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
    .AddScoped(x => x
    .GetRequiredService<IUrlHelperFactory>()
    .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.MaxDepth = 256; // Set maximum depth if needed
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Case-insensitive deserialization
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use default naming convention
    options.JsonSerializerOptions.AllowTrailingCommas = true; // Allow trailing commas in JSON
    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip; // Skip comments in JSON
    options.JsonSerializerOptions.DictionaryKeyPolicy = null; // Use default dictionary key policy
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Enforce HSTS
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Ensure to configure the middleware correctly
app.Use(async (context, next) =>
{
    if (!context.Request.IsHttps)
    {
        var withHttps = "https://" + context.Request.Host + context.Request.Path;
        context.Response.Redirect(withHttps);
        return;
    }

    // Limit the maximum request body size
    var maxLength = 102400; // Max length in bytes
    var buffer = new MemoryStream();
    var bytesRead = 0;
    var bufferBytesRead = 0;
    var readBuffer = new byte[4096]; // 4KB read buffer
    while ((bytesRead = await context.Request.Body.ReadAsync(readBuffer, 0, readBuffer.Length)) > 0)
    {
        if (bufferBytesRead + bytesRead > maxLength)
        {
            // If reading more bytes would exceed the max length, truncate the read
            bytesRead = (int)(maxLength - bufferBytesRead);
        }
        await buffer.WriteAsync(readBuffer, 0, bytesRead);
        bufferBytesRead += bytesRead;
        if (bufferBytesRead >= maxLength)
        {
            // If we've reached the max length, stop reading
            break;
        }
    }
    buffer.Seek(0, SeekOrigin.Begin);
    context.Request.Body = buffer;

    await next();
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapDefaultControllerRoute();
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Signinuser}/{id?}");
});

app.Run();
