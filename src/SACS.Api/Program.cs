using Microsoft.AspNetCore.Builder;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using SACS.Infrastructure.Db;
using SACS.Infrastructure.Repos;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using static Microsoft.AspNetCore.Http.Results;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ---------- Serilog: configurar logger antes de construir el host ----------
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // permite configurar desde appsettings
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/sacs-api-.log", rollingInterval: Serilog.RollingInterval.Day)
    .CreateLogger();

// Usar Serilog como logging provider
builder.Host.UseSerilog();

// ---------- Servicios ----------
builder.Services.AddSingleton<DbFactory>();
builder.Services.AddScoped<IActividadesEventosRepository, ActividadesEventosRepository>();
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();

// Registrar autom�ticamente implementaciones *Repository que implementen I*Repository
var repoAssembly = typeof(ActividadesEventosRepository).Assembly;
var repoTypes = repoAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));
foreach (var impl in repoTypes)
{
    var iface = impl.GetInterfaces().FirstOrDefault(i => i.Name == "I" + impl.Name);
    if (iface != null)
    {
        builder.Services.AddScoped(iface, impl);
    }
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => {
        var cfg = builder.Configuration.GetSection("Jwt");
        o.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = cfg["Issuer"],
            ValidAudience = cfg["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Key"]!))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SACS Dapper Repos API", Version = "v1" });
    var s = new OpenApiSecurityScheme {
        Name = "Authorization",
        Description = "Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };
    c.AddSecurityDefinition("Bearer", s);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement { { s, Array.Empty<string>() } });
});

// Registrar pol�tica CORS con nombres (evita problema de ICorsService y controla origenes vac�os)
builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultCors", policy =>
    {
        var origins = builder.Configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>();
        if (origins.Length > 0)
            policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
        else
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// A�adir Health Checks b�sicos (extender con checks reales: DB, Redis, etc.)
builder.Services.AddHealthChecks();

try
{
    Log.Information("Starting web host");

    var app = builder.Build();

    // Aplicar la pol�tica CORS registrada
    app.UseCors("DefaultCors");

    // Seguridad y HTTPS en producci�n
    if (!app.Environment.IsDevelopment())
    {
        app.UseHsts();
        app.UseHttpsRedirection();
    }

    // Registrar middleware de logging de Serilog
    app.UseSerilogRequestLogging();

    // Redirigir la ra�z a /swagger (�til para no obtener 404 en /)
    app.MapGet("/", () => Results.Redirect("/swagger"));

    // Habilitar archivos estáticos para que podamos inyectar JS personalizado en Swagger UI
    app.UseStaticFiles();

    // Habilitar Swagger UI (se inyectará un JS que solicita el token y añade Authorization automáticamente)
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "SACS Dapper Repos API v1");
        c.RoutePrefix = "swagger";
        // Inject custom JS that will automatically request /auth/token and set the Authorization header
        c.InjectJavascript("/swagger-ui/swagger-custom.js");
    });

    app.UseAuthentication();
    app.UseAuthorization();

    // Health endpoint p�blico (puedes protegerlo si lo deseas)
    app.MapHealthChecks("/health");

    app.MapPost("/auth/token", () => {
        // Emit a development JWT using the configured Jwt settings so the bearer validation accepts it
        var cfg = builder.Configuration.GetSection("Jwt");
        var keyString = cfg["Key"] ?? "dev_key_please_set_in_configuration";
        var issuer = cfg["Issuer"] ?? "SACS";
        var audience = cfg["Audience"] ?? "SACS_CLIENT";

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: null,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Ok(new { access_token = tokenString });
    });

    GeneratedEndpoints.Map(app);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public static class GeneratedEndpoints {
    public static void Map(WebApplication app) {
        SACS.Api.Endpoints.ActividadesEventosEndpoints.Map(app);
        SACS.Api.Endpoints.AsistenciaEndpoints.Map(app);
        SACS.Api.Endpoints.AsistenciasManualesEndpoints.Map(app);
        SACS.Api.Endpoints.CitacionesEndpoints.Map(app);
        SACS.Api.Endpoints.EvaluacionEndpoints.Map(app);
        SACS.Api.Endpoints.EvaluacionExternaEndpoints.Map(app);
        SACS.Api.Endpoints.MatriculasEndpoints.Map(app);
        SACS.Api.Endpoints.NotasEndpoints.Map(app);
        SACS.Api.Endpoints.NotasExternaEndpoints.Map(app);
        SACS.Api.Endpoints.NotasManualesEndpoints.Map(app);
        SACS.Api.Endpoints.PeriodosEndpoints.Map(app);
        SACS.Api.Endpoints.PeriodosXGruposEndpoints.Map(app);
        SACS.Api.Endpoints.PeriodosXHorariosEndpoints.Map(app);
        SACS.Api.Endpoints.PlaneamientoDidacticoEndpoints.Map(app);
        SACS.Api.Endpoints.PlaneamientoDidacticoObjetivoEndpoints.Map(app);
        SACS.Api.Endpoints.PlaneamientoDidacticoObjetivoCritEndpoints.Map(app);
        SACS.Api.Endpoints.PlaneamientoDidacticoObjetivoInstEndpoints.Map(app);
    }
}
