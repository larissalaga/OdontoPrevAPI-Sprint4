using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Configuration;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Mappings;
using OdontoPrevAPI.MlModels;
using OdontoPrevAPI.Repositories.Implementations;
using OdontoPrevAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Initialize ConfigurationManager with the configuration
ConfigManager.Instance.Initialize(builder.Configuration);

// Registrando o AutoMapper com o perfil de mapeamento
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.EnableAnnotations();
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseOracle(ConfigManager.Instance.GetConnectionString("OracleConnection"));
});

// Registering repositories as singletons
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IDentistaRepository, DentistaRepository>();
builder.Services.AddScoped<IPlanoRepository, PlanoRepository>();
builder.Services.AddScoped<IExtratoPontosRepository, ExtratoPontosRepository>();
builder.Services.AddScoped<IRaioXRepository, RaioXRepository>();
builder.Services.AddScoped<IAnaliseRaioXRepository, AnaliseRaioXRepository>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();
builder.Services.AddScoped<IPerguntasRepository, PerguntasRepository>();
builder.Services.AddScoped<IRespostasRepository, RespostasRepository>();
builder.Services.AddScoped<IPacienteDentistaRepository, PacienteDentistaRepository>();

// Initialize ML service when the application starts
// Register Generative AI service
builder.Services.AddSingleton<GenerativeAIService>();
builder.Services.AddScoped<MlService>();
builder.Services.AddHostedService<MlInitializationService>();


builder.Services.AddDataProtection();
builder.Services.AddSingleton(ConfigManager.Instance);

// --- Google Authentication configuration ---
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie()
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    // Force account selection every time
    options.Events.OnRedirectToAuthorizationEndpoint = context =>
    {
        var prompt = "select_account";
        var redirectUri = context.RedirectUri;
        // Add prompt=select_account to the query string
        if (!redirectUri.Contains("prompt="))
        {
            redirectUri += (redirectUri.Contains("?") ? "&" : "?") + "prompt=" + prompt;
        }
        context.Response.Redirect(redirectUri);
        return Task.CompletedTask;
    };
});
// --- End Google Authentication configuration ---


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Serve static files from StaticFiles folder
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
        RequestPath = ""
    });

    // Require authentication for Swagger UI and Swagger JSON
    app.UseWhen(
        context => context.Request.Path.StartsWithSegments("/swagger"),
        appBuilder =>
        {
            appBuilder.UseAuthentication();
            appBuilder.UseAuthorization();
            appBuilder.Use(async (context, next) =>
            {
                if (!context.User.Identity?.IsAuthenticated ?? true)
                {
                    await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme);
                    return;
                }
                await next();
            });

            // Place SwaggerUI here so it's protected
            appBuilder.UseSwaggerUI(options =>
            {
                options.InjectJavascript("/swagger-user.js");
            });
        });
}

app.UseHttpsRedirection();

app.UseAuthentication(); // <-- Add this line before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }