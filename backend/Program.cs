using System.Security.Claims;
using System.Text.Json.Serialization;
using backend.DbContexts;
using backend.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors(options => {
//     options.AddPolicy(myAllowSpecificOrigins,
//                       policy => 
//                       {
//                         policy.WithOrigins()
//                             .AllowAnyOrigin()
//                             .AllowAnyHeader()
//                             .AllowAnyMethod();
//                       });
//     options.AddPolicy("any",
//                     builder => builder
//                     .AllowAnyMethod()
//                     .AllowAnyHeader()

//                     .AllowCredentials()
//                     .SetIsOriginAllowed(hostName => true));
// });

builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder => 
            builder.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
        );
    });

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDistributedMemoryCache();
// builder.Services.AddSession(options => 
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(2);
// });

builder.Services.AddLogging();

builder.Services.AddDbContext<AutomobileContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("AutomobileContext"));
});

builder.Services.AddScoped<IAutomobileRepository, AutomobileRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.None;
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("OnlySupplier", policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(ClaimTypes.Role, "supplier");
    });
});
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// app.UseSession();
// Configure the HTTP request pipeline.
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
