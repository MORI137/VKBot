using Microsoft.AspNetCore.Mvc.Formatters;
using ProtoBuf.Meta;
using Serilog;
using System.Text.Json;
using VKBot.Filters;
using VkNet;
using VkNet.Abstractions;
using VkNet.Enums.StringEnums;
using VkNet.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IVkApi>(sp =>
{
    var api = new VkApi();
    api.Authorize(new ApiAuthParams { AccessToken = builder.Configuration["Config:AccessToken"] });
    return api;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new VkMessageBinderProvider());
});

builder.Host.UseSerilog();

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<ModifyRequestBodyMiddleware>();

app.MapControllers();

app.Run();

Log.CloseAndFlush();