using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
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
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // If you want case-insensitive deserialization
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // If you want camel case naming convention
    options.JsonSerializerOptions.AllowTrailingCommas = true; // Allow trailing commas in JSON
    options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip; // Skip comments in JSON
    options.JsonSerializerOptions.DictionaryKeyPolicy = null; // If you want the dictionary key policy to be null
});

var app = builder.Build();

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

app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Signinuser}/{id?}");

app.UseExceptionHandler("/Home/Error");
app.UseHsts();
app.UseHttpsRedirection();

app.Run();

public class LimitedStream : Stream
{
    private readonly Stream _innerStream;
    private readonly long _maxLength;
    private long _totalBytesRead;

    public LimitedStream(Stream innerStream, long maxLength, MemoryStream buffer)
    {
        _innerStream = innerStream;
        _maxLength = maxLength;
        Buffer = buffer;
    }

    public MemoryStream Buffer { get; }

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