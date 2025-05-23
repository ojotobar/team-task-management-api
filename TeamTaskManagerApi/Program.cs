using Contracts;
using Serilog;
using TeamTaskManagerApi.Configurations;
using TeamTaskManagerApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
//Configre logging
LogConfigurations.ConfigureLogging();
builder.Host.UseSerilog();
// Add services to the container.
builder.Services.ConfigureController();
builder.Services.ConfigreDbContext(builder.Configuration);
builder.Services.ConfigureVersioning();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);

var app = builder.Build();

//Configure global exception handler
var logger = app.Services.GetRequiredService<IAppLogger>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
