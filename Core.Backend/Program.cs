using Core.AuthLib;
using Core.Backend;
using Core.Backend.Services;
using Core.Database;
using CorePlugin.Plugin.Services;
using Microsoft.EntityFrameworkCore;

PluginLoader.LoadPlugins("plugins");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(o => { o.AddSwaggerGenHeader(); });

var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<CoreContext>(db => db.UseSqlite(connectionString));
builder.Services.AddScoped<UserFavoritesService>();
builder.Services.AddHostedService<DatabaseBackgroundService>();

builder.AddHeaderAuth();

var app = builder.InjectBuilderToPlugin().Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder => policyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(app.Configuration["MainframeOrigin"])
);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.InjectAppToPlugin().Run();
