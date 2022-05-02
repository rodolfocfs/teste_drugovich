using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WebAPIGerenciadorDeClientes.Models;
using WebAPIGerenciadorDeClientes.Services.Contracts;
using WebAPIGerenciadorDeClientes.Services.Concrets;
using WebAPIGerenciadorDeClientes.Common.Middlewares.SessionCache;
using WebAPIGerenciadorDeClientes.Common.Scope;
using Microsoft.AspNetCore.ResponseCompression;
using WebAPIGerenciadorDeClientes.Repositories;
using WebAPIGerenciadorDeClientes.Services.Mappers;
using WebAPIGerenciadorDeClientes.Common.Exceptions.Middleware;
using WebAPIGerenciadorDeClientes.Common.Middlewares.Localization;
using WebAPIGerenciadorDeClientes.Common.Middlewares;
using FluentValidation.AspNetCore;
using WebAPIGerenciadorDeClientes.Services.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ScopeInformation>();
builder.Services.AddScoped<HttpClient>();
builder.Services.AddDistributedMemoryCache();
builder.Services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
builder.Services.AddResponseCompression(opt => opt.Providers.Add<GzipCompressionProvider>());

builder.Services.AddAutoMapper(typeof(ApiProfile));

builder.Services.AddScoped<IGerenteRepository, GerenteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IGrupoRepository, GrupoRepository>();

builder.Services.AddScoped<IGerenteService, GerenteService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IGrupoService, GrupoService>();

var key = Encoding.ASCII.GetBytes(builder.Configuration["Token:Secret"]);

builder.Services
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<ModelContext>(options => {

        options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
    }
);

builder.Services.AddMvc()
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssemblyContaining<GerenteCreateRequestValidator>();
                    x.RegisterValidatorsFromAssemblyContaining<ClienteRequestValidator>();
                    x.RegisterValidatorsFromAssemblyContaining<UpdateClienteRequestValidator>();
                    x.RegisterValidatorsFromAssemblyContaining<GrupoRequestValidator>();
                    x.ImplicitlyValidateChildProperties = true;
                });

var app = builder.Build();

app.UsePtBrLocalization();

app.Use((context, next) =>
{
    context.Request.EnableBuffering();
    return next();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseResponseCompression();
app.UseRouting();
app.UseAuthorization();
app.UseApiExceptionHandler();
app.UseEnvironmentMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/index.html", req => req.Response.WriteAsync("<html><head></head><body><h1>Sotreq - Gerenciamento de Clientes - API<body><html>"));
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Default}/{action}/{id?}"
        );
});

app.UseHttpsRedirection();
app.UseMiddleware<SessionCacheMiddleware>();

app.Run();
