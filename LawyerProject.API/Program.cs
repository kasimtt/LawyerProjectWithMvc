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
//var connectionString = ConnectionString.DefaultConnection; //connection string icin bir static sýnýf yazýlabilir.

// Add services to the container.
#region Controller - Fluent Validation - Json
// IServiceCollection arabirimine Controllers hizmetini ekler. Bu, Web API projesinde denetleyicilerin (Controller'larýn) kullanýlmasýný saðlar.
builder.Services.AddControllers(opt =>
{
    //opt.Filters.Add(typeof(UserActivity_));
    opt.Filters.Add<ValidationFilter>();
    opt.Filters.Add<RolePermissionFilter>();
})
    // FluentValidation kütüphanesinin yapýlandýrmasýný ekler. 
    .AddFluentValidation(configuration =>
    {
        // Veri anotasyonlarý tabanlý doðrulama özelliðini devre dýþý býrakýr. Bu, veri modellerinin üzerindeki DataAnnotations doðrulama özelliklerini kullanmadan sadece FluentValidation kurallarýný kullanmayý saðlar.
        configuration.DisableDataAnnotationsValidation = true;

        // YoneticiValidator sýnýfýnýn bulunduðu derlemedeki doðrulayýcýlarý FluentValidation yapýlandýrmasýna kaydeder. Bu þekilde, belirtilen derlemedeki tüm doðrulayýcýlar otomatik olarak kullanýlabilir hale gelir.
        //configuration.RegisterValidatorsFromAssemblyContaining<YoneticiValidator>(); -->validatorleri yazdýktan sonra eklicez

        //otomatik doðrulama özelliðini etkinleþtirir. Bu, bir HTTP isteði alýndýðýnda, doðrulama kurallarýnýn otomatik olarak uygulanmasýný saðlar.
        configuration.AutomaticValidationEnabled = true;
    })

    //JSON serileþtirme yapýlandýrmasýný ekler.
    .AddJsonOptions(configurations =>
    {
        // Serileþtirmesinde döngüsel referanslarý iþlemek için referans iþleyiciyi ayarlar. IgnoreCycles, döngüsel referanslarý görmezden gelerek olasý bir sonsuz döngüyü önler.
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
    // Varsayýlan API sürümünü ayarlar. Burada 1.0 sürümü, projenin varsayýlan API sürümü olarak belirlenir.
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Belirtilmeyen durumlarda varsayýlan API sürümünün kullanýlmasýný saðlar. Yani istemci bir API sürümü belirtmezse, options.DefaultApiVersion ile belirtilen varsayýlan sürüm kullanýlýr.
    options.AssumeDefaultVersionWhenUnspecified = true;

    // API sürümlerini yanýtta raporlama ayarýný etkinleþtirir. Bu, API yanýtlarýnda kullanýlan sürüm bilgisini gönderir.
    options.ReportApiVersions = true;
});

// IServiceCollection arabirimine VersionedApiExplorer hizmetini ekler. 
builder.Services.AddVersionedApiExplorer(options =>
{
    // API grup adý biçimini belirler. 'v'VVV formatý kullanýlarak grup adlarý oluþturulur. VVV, API sürümünü temsil eden yer tutucudur.
    options.GroupNameFormat = "'v'VVV";

    // API sürümünü URL içinde yerine koyma ayarýný etkinleþtirir. Böylece, API isteklerinde URL içindeki sürüm belirtilmiþ olur.
    options.SubstituteApiVersionInUrl = true;
});
#endregion

#region Swagger
// Swagger yapýlandýrma seçeneklerini SwaggerGenOptions için yapýlandýran ConfigureSwaggerOptions sýnýfýný IConfigureOptions arabirimine ekler. Bu, Swagger belgelendirmesinin yapýlandýrýlmasýný saðlar.
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// IServiceCollection arabirimine SwaggerGen hizmetini ekler.
builder.Services.AddSwaggerGen(options =>
{
    // Mevcut projenin XML belgelendirme dosyasýnýn adýný oluþturur. Bu dosya, projenin içinde API hakkýnda detaylý bilgileri içerir.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    // XML belgelendirme dosyasýnýn tam yolunu oluþturur. AppContext.BaseDirectory, uygulamanýn çalýþtýðý dizini temsil eder.
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Swagger belgelendirmesine XML belgelendirme dosyasýný dahil etme ayarýný yapar. Bu sayede, API Controller'larýndaki örnekler, parametreler ve dönüþ deðerleri gibi detaylý açýklamalarý Swagger belgelerine ekler.
});
#endregion

#region Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new()
        {
            ValidateAudience = true, //oluþturulacak tokenin kimlerin/ hangi sitelerin/ hangi originlerin kullanacaðýný belirlediðimiz deðerdir(www.lawyerclient.com)
            ValidateIssuer = true,  //oluþturulacak  tokenin kimin daðýtýðýmýný ifade eden alandýr(www.lawyerapi.com)
            ValidateLifetime = true, //oluþturulacak tokenin süresini kontrol edecek alandýr
            ValidateIssuerSigningKey = true, //üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden security key verisinin doðrulanmasýdýr 

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name
        };
    });
#endregion


#region Cors
// IServiceCollection arabirimine CORS (Cross-Origin Resource Sharing) hizmetini ekler. CORS, web uygulamalarýnýn farklý kaynaklardan gelen isteklere izin vermesini saðlayan bir mekanizmadýr.
// CORS hizmetini eklemek, Web API'nin farklý etki alanlarýndan gelen istekleri kabul etmesini ve gerekirse yanýtlara uygun CORS baþlýklarýný eklemesini saðlar. Bu þekilde, Web API'ye dýþ kaynaklardan eriþim saðlanabilir.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));



#endregion
#region DbContext
// IServiceCollection arabirimine XXXContext tipinde bir veritabaný baðlamýný (DbContext) ekler.
builder.Services.AddDbContext<LawyerProjectContext>(options =>
{
    // SQL Server veritabaný saðlayýcýsýný kullanarak veritabaný baðlamýnýn yapýlandýrýlmasýný yapar. connectionString parametresi, SQL Server baðlantý dizesini temsil eder. 
    // b => b.MigrationsAssembly("SalihPoc.Api") ifadesi, veritabaný migrasyonlarýný uygulamak için kullanýlacak olan migrasyon derlemesinin adýný belirtir. Bu ad, "SalihPoc.Api" olarak belirtilmiþtir.

    options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly("LawyerProject.Persistence"));
});
builder.Services.AddIdentity<AppUser, AppRole>(opt =>  // test ortamýnda olduðu için password configurasyonlarý sýnýflandýrýldý
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 2;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<LawyerProjectContext>().AddDefaultTokenProviders(); // identity iþlemlerine dair tüm storage iþlemleri burada bulunur

#endregion   

builder.Services.AddContainerWithDependenciesApplication();
builder.Services.AddContainerWithDependenciesPersistence();
builder.Services.AddContainerWithDependenciesInfrastucture();
builder.Services.AddStorage<LocalStorage>();  // istediðimiz storage burada aktif edebiliriz 
//builder.Services.AddStorage<AzureStorage>();
builder.Services.AddSignalRServices();

builder.Services.AddAutoMapper(typeof(CasesProfile)); //ilgili assemblydeki herhangi bir sýnýfý girmemiz yeterli olcaktýr

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
