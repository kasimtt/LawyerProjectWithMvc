using FluentValidation.AspNetCore;
using LawyerProject.API.Configurations.ColumnWriters;
using LawyerProject.API.Filters;
using LawyerProject.Persistence.Filters;
using Microsoft.AspNetCore.HttpLogging;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Serilog.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using LawyerProject.Application;
using LawyerProject.Persistence;
using LawyerProject.Infrastructure;
using LawyerProject.SignalR;
using LawyerProject.Application.Mappers;
using LawyerProject.API.Extensions;
using Serilog.Context;
using LawyerProject.Domain.Entities.Identity;
using LawyerProject.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LawyerProject.Infrastructure.Services.Storage.Local;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.


#region Controller - Fluent Validation - Json
// IServiceCollection arabirimine Controllers hizmetini ekler. Bu, Web API projesinde denetleyicilerin (Controller'lar�n) kullan�lmas�n� sa�lar.
builder.Services.AddControllersWithViews(opt =>
{
    //opt.Filters.Add(typeof(UserActivity_));
    opt.Filters.Add<ValidationFilter>();
    opt.Filters.Add<RolePermissionFilter>();
})
    // FluentValidation k�t�phanesinin yap�land�rmas�n� ekler. 
    .AddFluentValidation(configuration =>
    {
        // Veri anotasyonlar� tabanl� do�rulama �zelli�ini devre d��� b�rak�r. Bu, veri modellerinin �zerindeki DataAnnotations do�rulama �zelliklerini kullanmadan sadece FluentValidation kurallar�n� kullanmay� sa�lar.
        configuration.DisableDataAnnotationsValidation = true;

        // YoneticiValidator s�n�f�n�n bulundu�u derlemedeki do�rulay�c�lar� FluentValidation yap�land�rmas�na kaydeder. Bu �ekilde, belirtilen derlemedeki t�m do�rulay�c�lar otomatik olarak kullan�labilir hale gelir.
        //configuration.RegisterValidatorsFromAssemblyContaining<YoneticiValidator>(); -->validatorleri yazd�ktan sonra eklicez

        //otomatik do�rulama �zelli�ini etkinle�tirir. Bu, bir HTTP iste�i al�nd���nda, do�rulama kurallar�n�n otomatik olarak uygulanmas�n� sa�lar.
        configuration.AutomaticValidationEnabled = true;
    })

    //JSON serile�tirme yap�land�rmas�n� ekler.
    .AddJsonOptions(configurations =>
    {
        // Serile�tirmesinde d�ng�sel referanslar� i�lemek i�in referans i�leyiciyi ayarlar. IgnoreCycles, d�ng�sel referanslar� g�rmezden gelerek olas� bir sonsuz d�ng�y� �nler.
        configurations.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
#endregion
#region Logging
SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "UserName";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "UserName";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };



Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("DefaultConnection"),
     sinkOptions: new MSSqlServerSinkOptions
     {
         AutoCreateSqlTable = true,
         TableName = "Logs",
     },
     appConfiguration: null,
     columnOptions: columnOpt
    )
    .Enrich.FromLogContext()
    .Enrich.With<CustomUserNameColumn>()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});
#endregion


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Cookie'nin ge�erlilik s�resi
        options.SlidingExpiration = true; // S�re bitti�inde otomatik uzat�lmas�n� sa�lar
    });

#region Dependency Injection
builder.Services.AddContainerWithDependenciesApplication();
builder.Services.AddContainerWithDependenciesPersistence();
builder.Services.AddContainerWithDependenciesInfrastucture();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddSignalRServices();
#endregion
#region DbContext
// IServiceCollection arabirimine XXXContext tipinde bir veritaban� ba�lam�n� (DbContext) ekler.
builder.Services.AddDbContext<LawyerProjectContext>(options =>
{
    // SQL Server veritaban� sa�lay�c�s�n� kullanarak veritaban� ba�lam�n�n yap�land�r�lmas�n� yapar. connectionString parametresi, SQL Server ba�lant� dizesini temsil eder. 
    // b => b.MigrationsAssembly("SalihPoc.Api") ifadesi, veritaban� migrasyonlar�n� uygulamak i�in kullan�lacak olan migrasyon derlemesinin ad�n� belirtir. Bu ad, "SalihPoc.Api" olarak belirtilmi�tir.

    options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("LawyerProject.Persistence"));
});
builder.Services.AddIdentity<AppUser, AppRole>(opt =>  // test ortam�nda oldu�u i�in password configurasyonlar� s�n�fland�r�ld�
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 2;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<LawyerProjectContext>().AddDefaultTokenProviders(); // identity i�lemlerine dair t�m storage i�lemleri burada bulunur

#endregion   


builder.Services.AddAutoMapper(typeof(CasesProfile));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseRouting();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();




app.Use(async (LawyerProjectContext, next) =>
{
    var username = LawyerProjectContext.User?.Identity?.IsAuthenticated != null || true ? LawyerProjectContext.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);
    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Adverts}/{action=Index}/{id?}");

app.Run();
