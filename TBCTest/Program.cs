using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Reflection;
using TBCTest.Data;
using TBCTest.Filters;
using TBCTest.Managers;
using TBCTest.Mapping;
using TBCTest.Middleware;
using TBCTest.Repositories;
using TBCTest.Services;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controllers + validation filter
builder.Services.AddControllers(o => o.Filters.Add<ValidateModelAttribute>());

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TBCTest API", Version = "v1" });
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var path = Path.Combine(AppContext.BaseDirectory, xml);
    if (File.Exists(path)) c.IncludeXmlComments(path);
});

// DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityManager, CityManager>();
builder.Services.AddScoped<ILocalizationRepository, LocalizationRepository>();
builder.Services.AddScoped<ILocalizationManager, LocalizationManager>();
builder.Services.AddScoped<IDbLocalizationService, DbLocalizationService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Localization options
var cultures = new[] { "en-US", "ka-GE" };
var locOpts = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = cultures.Select(c => new CultureInfo(c)).ToList(),
    SupportedUICultures = cultures.Select(c => new CultureInfo(c)).ToList()
};
locOpts.RequestCultureProviders.Insert(0, new AcceptLanguageHeaderRequestCultureProvider());

var app = builder.Build();

// Dev‐only exception page & Swagger
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TBCTest API V1"));
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRequestLocalization(locOpts);
app.UseMiddleware<ExceptionLoggingMiddleware>();
app.UseAuthorization();
app.MapControllers();

// Seed localization keys
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await LocalizationSeeder.SeedAsync(db);

app.Run();
