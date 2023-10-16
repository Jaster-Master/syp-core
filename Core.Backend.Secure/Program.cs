using Core.AuthLib;
using Core.AuthLib.Services;
using Core.Backend.Secure.Services;
using Core.Ldap.Implementation;
using Core.Ldap.Interface;
using Core.Secure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("SqliteConnection");
builder.Services.AddDbContext<CoreSecureContext>(db => db.UseSqlite(connectionString));

builder.Services.AddHostedService<CsvReaderService>();
builder.Services.AddScoped<CredService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddSingleton(RsaService.ImportRSAKey("./keys/" + builder.Configuration["RSA:private-key"], true));

builder.Services.Configure<LdapConfiguration>(builder.Configuration.GetSection("LDAPConfiguration"));
builder.Services.AddTransient<ILdapClient, LdapClient>();

builder.Services.AddTransient<WebUntisService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCookieAuth(true);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CoreSecureContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var allowedOrigins = app.Configuration.GetSection("AllowedOrigins").Get<string[]>();
app.UseCors(policyBuilder => policyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins(allowedOrigins)
);
var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Allowed origins:");
foreach (var allowedOrigin in allowedOrigins)
{
    logger.LogInformation(allowedOrigin);
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
