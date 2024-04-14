using System.Text.Json.Serialization;
using backend.DbContexts;
using backend.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var initUtil = new InitUtil(builder);

initUtil.AddCors();

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();

initUtil.AddSwagger();

builder.Services.AddLogging();

builder.Services.AddDbContext<AutomobileContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("AutomobileContext"));
});

initUtil.AddServices();

initUtil.AddAuthentication();
initUtil.AddAuthorization();

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
