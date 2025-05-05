using TBCTest.Data;
using Microsoft.EntityFrameworkCore;
using TBCTest.Managers;
using TBCTest.Repositories;
using TBCTest.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonManager, PersonManager>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICityManager, CityManager>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en-US", "ka-GE" };
    options.SetDefaultCulture("en-US")
           .AddSupportedCultures(supportedCultures)
           .AddSupportedUICultures(supportedCultures);
});
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
Console.OutputEncoding = System.Text.Encoding.UTF8;
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
