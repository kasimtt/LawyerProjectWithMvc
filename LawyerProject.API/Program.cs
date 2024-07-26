using FluentValidation.AspNetCore;
using LawyerProject.API.Extensions;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using LawyerProject.AspNetCore.Infrastracture;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using LawyerProject.Persistence.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using LawyerProject.Application;
using LawyerProject.Persistence;
using LawyerProject.Application.Mappers;
using LawyerProject.Persistence.Filters;
using LawyerProject.Infrastructure;
using LawyerProject.Infrastructure.Services.Storage.Local;
using LawyerProject.Infrastructure.Services.Storage.Azure;
using LawyerProject.Domain.Entities.Identity;
using LawyerProject.API.Configurations.ColumnWriters;
using Serilog.Core;
using Microsoft.AspNetCore.HttpLogging;
using Serilog.Context;
using LawyerProject.SignalR;
using Microsoft.AspNetCore.Identity;
using LawyerProject.API.Filters;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//var connectionString = ConnectionString.DefaultConnection; //connection string icin bir static s�n�f yaz�labilir.

// Add services to the container.
#region Controller - Fluent Validation - Json
// IServiceCollection arabirimine Controllers hizmetini ekler. Bu, Web API projesinde denetleyicilerin (Controller'lar�n) kullan�lmas�n� sa�lar.
builder.Services.AddControllers(opt =>
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


#region Api Versioning & Api Explorer
// IServiceCollection arabirimine ApiVersioning hizmetini ekler. 
builder.Services.AddApiVersioning(options =>
{
    // Varsay�lan API s�r�m�n� ayarlar. Burada 1.0 s�r�m�, projenin varsay�lan API s�r�m� olarak belirlenir.
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Belirtilmeyen durumlarda varsay�lan API s�r�m�n�n kullan�lmas�n� sa�lar. Yani istemci bir API s�r�m� belirtmezse, options.DefaultApiVersion ile belirtilen varsay�lan s�r�m kullan�l�r.
    options.AssumeDefaultVersionWhenUnspecified = true;

    // API s�r�mlerini yan�tta raporlama ayar�n� etkinle�tirir. Bu, API yan�tlar�nda kullan�lan s�r�m bilgisini g�nderir.
    options.ReportApiVersions = true;
});

// IServiceCollection arabirimine VersionedApiExplorer hizmetini ekler. 
builder.Services.AddVersionedApiExplorer(options =>
{
    // API grup ad� bi�imini belirler. 'v'VVV format� kullan�larak grup adlar� olu�turulur. VVV, API s�r�m�n� temsil eden yer tutucudur.
    options.GroupNameFormat = "'v'VVV";

    // API s�r�m�n� URL i�inde yerine koyma ayar�n� etkinle�tirir. B�ylece, API isteklerinde URL i�indeki s�r�m belirtilmi� olur.
    options.SubstituteApiVersionInUrl = true;
});
#endregion

#region Swagger
// Swagger yap�land�rma se�eneklerini SwaggerGenOptions i�in yap�land�ran ConfigureSwaggerOptions s�n�f�n� IConfigureOptions arabirimine ekler. Bu, Swagger belgelendirmesinin yap�land�r�lmas�n� sa�lar.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// IServiceCollection arabirimine SwaggerGen hizmetini ekler.
builder.Services.AddSwaggerGen(options =>
{
    // Mevcut projenin XML belgelendirme dosyas�n�n ad�n� olu�turur. Bu dosya, projenin i�inde API hakk�nda detayl� bilgileri i�erir.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    // XML belgelendirme dosyas�n�n tam yolunu olu�turur. AppContext.BaseDirectory, uygulaman�n �al��t��� dizini temsil eder.
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Swagger belgelendirmesine XML belgelendirme dosyas�n� dahil etme ayar�n� yapar. Bu sayede, API Controller'lar�ndaki �rnekler, parametreler ve d�n�� de�erleri gibi detayl� a��klamalar� Swagger belgelerine ekler.
});
#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateAudience = true, //olu�turulacak tokenin kimlerin/ hangi sitelerin/ hangi originlerin kullanaca��n� belirledi�imiz de�erdir(www.lawyerclient.com)
            ValidateIssuer = true,  //olu�turulacak  tokenin kimin da��t���m�n� ifade eden aland�r(www.lawyerapi.com)
            ValidateLifetime = true, //olu�turulacak tokenin s�resini kontrol edecek aland�r
            ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulanmas�d�r 

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name
        };
    });
#endregion


#region Cors
// IServiceCollection arabirimine CORS (Cross-Origin Resource Sharing) hizmetini ekler. CORS, web uygulamalar�n�n farkl� kaynaklardan gelen isteklere izin vermesini sa�layan bir mekanizmad�r.
// CORS hizmetini eklemek, Web API'nin farkl� etki alanlar�ndan gelen istekleri kabul etmesini ve gerekirse yan�tlara uygun CORS ba�l�klar�n� eklemesini sa�lar. Bu �ekilde, Web API'ye d�� kaynaklardan eri�im sa�lanabilir.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));



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

builder.Services.AddContainerWithDependenciesApplication();
builder.Services.AddContainerWithDependenciesPersistence();
builder.Services.AddContainerWithDependenciesInfrastucture();
builder.Services.AddStorage<LocalStorage>();  // istedi�imiz storage burada aktif edebiliriz 
//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddSignalRServices();

builder.Services.AddAutoMapper(typeof(CasesProfile)); //ilgili assemblydeki herhangi bir s�n�f� girmemiz yeterli olcakt�r

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var provider = builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"{description.GroupName}/swagger.json",
                $"LawyerProject API {description.GroupName.ToUpperInvariant()}");
        }
    });

    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseCors();
app.UseAuthentication();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (LawyerProjectContext, next) =>
{
    var username = LawyerProjectContext.User?.Identity?.IsAuthenticated != null || true ? LawyerProjectContext.User.Identity.Name : null;
    LogContext.PushProperty("UserName", username);
    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();
