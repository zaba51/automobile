using System.Reflection;
using System.Security.Claims;
using System.Text;
using backend.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace backend.Utils
{
    public class InitUtil
    {

        private readonly WebApplicationBuilder _builder;

        public InitUtil(WebApplicationBuilder builder)
        {
            _builder = builder;
        }
        public void AddServices()
        {

            _builder.Services.AddScoped<IAutomobileRepository, AutomobileRepository>();
            _builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
            _builder.Services.AddScoped<IUserRepository, UserRepository>();
            _builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            _builder.Services.AddScoped<KafkaProducerService>();
            _builder.Services.AddScoped<KafkaConsumerService>();
            _builder.Services.AddScoped<UploadService>();
            _builder.Services.AddScoped<ICatalogService, CatalogService>();
            _builder.Services.AddScoped<IReservationService, ReservationService>();
        }

        public void AddCors()
        {
            _builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                        builder.SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                    );
                });
        }

        public void AddSwagger()
        {
            _builder.Services.AddSwaggerGen(setupAction =>
            {
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
        }

        public void AddAuthentication()
        {

            _builder.Services.AddAuthentication("Bearer")
                // .AddCookie(options =>
                // {
                //     options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                //     options.SlidingExpiration = true;
                //     options.Cookie.SameSite = SameSiteMode.Unspecified;
                // });
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _builder.Configuration["Authentication:Issuer"],
                        ValidAudience = _builder.Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(_builder.Configuration["Authentication:SecretForKey"]))
                    };
                });
        }

        public void AddAuthorization()
        {
            _builder.Services.AddAuthorization(options => {
                options.AddPolicy("OnlySupplier", policy => {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(ClaimTypes.Role, "supplier");
                });
            });
        }
    }
}