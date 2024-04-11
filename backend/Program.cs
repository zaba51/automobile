using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using backend.DbContexts;
using backend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder => 
            builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        );
    });


builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    setupAction.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    setupAction.AddSecurityDefinition("CinemanApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "CinemanApiBearerAuth" }
                }, new List<string>()
        }
    });
});

builder.Services.AddLogging();

builder.Services.AddDbContext<AutomobileContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("AutomobileContext"));
});

builder.Services.AddScoped<IAutomobileRepository, AutomobileRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<KafkaProducerService>();
builder.Services.AddScoped<KafkaConsumerService>();

builder.Services.AddAuthentication("Bearer")
    // .AddCookie(options =>
    // {
    //     options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
    //     options.SlidingExpiration = true;
    //     options.Cookie.SameSite = SameSiteMode.Unspecified;
    // });
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
                ValidAudience = builder.Configuration["Authentication:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("OnlySupplier", policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimTypes.Role, "supplier");
    });
});
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
