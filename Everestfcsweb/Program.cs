using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, x =>
{
    x.AccessDeniedPath = "/Account/AccessDenied/";
    x.LoginPath = "/Account/Signinuser/";
    x.Cookie.HttpOnly = true;
    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>()
    .AddScoped(x => x.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext));

builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.MaxDepth = 256;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.AllowTrailingCommas = true;
    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
});

var app = builder.Build();

// Middleware to enforce HTTPS and limit request body size
app.Use(async (context, next) =>
{
    if (!context.Request.IsHttps)
    {
        var withHttps = $"https://{context.Request.Host}{context.Request.Path}";
        context.Response.Redirect(withHttps);
        return;
    }

    const int maxLength = 102400; // Max length in bytes
    using var buffer = new MemoryStream();
    var totalBytesRead = 0;
    var readBuffer = new byte[4096]; // 4KB read buffer

    while (totalBytesRead < maxLength && context.Request.Body.CanRead)
    {
        var bytesRead = await context.Request.Body.ReadAsync(readBuffer.AsMemory(0, Math.Min(4096, maxLength - totalBytesRead)));
        if (bytesRead == 0) break;

        await buffer.WriteAsync(readBuffer.AsMemory(0, bytesRead));
        totalBytesRead += bytesRead;
    }

    buffer.Seek(0, SeekOrigin.Begin);
    context.Request.Body = buffer;
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Signinuser}/{id?}");

app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.Run();

public class LimitedStream : Stream
{
    private readonly Stream _innerStream;
    private readonly long _maxLength;
    private long _totalBytesRead;

    public LimitedStream(Stream innerStream, long maxLength)
    {
        _innerStream = innerStream;
        _maxLength = maxLength;
    }

    public override bool CanRead => _innerStream.CanRead;
    public override bool CanSeek => _innerStream.CanSeek;
    public override bool CanWrite => _innerStream.CanWrite;
    public override long Length => _innerStream.Length;
    public override long Position
    {
        get => _innerStream.Position;
        set => _innerStream.Position = value;
    }

    public override void Flush() => _innerStream.Flush();
    public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);
    public override void SetLength(long value) => _innerStream.SetLength(value);

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (_totalBytesRead >= _maxLength)
            return 0;

        int bytesRead = _innerStream.Read(buffer, offset, (int)Math.Min(count, _maxLength - _totalBytesRead));
        _totalBytesRead += bytesRead;
        return bytesRead;
    }

    public override void Write(byte[] buffer, int offset, int count) => _innerStream.Write(buffer, offset, count);
}
