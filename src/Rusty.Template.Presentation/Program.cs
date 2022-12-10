using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Middlewares;
using Rusty.Template.Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfigurations();

// Add logging
builder.Host.AddSerilog();

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
// Build app
var app = builder.Build();
// set Serilog request logging
app.UseSerilogRequestLogging(configure =>
{
    configure.MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} ({UserId} {UserName} responded {StatusCode} in {Elapsed:0.0000}ms)";
});
//Prepare db
// if (app.Environment.IsStaging())
await app.Services.MigrateDatabaseAsync();
await app.Services.InitializeDatabaseDataAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var swaggerDoc = app.Configuration.GetSection("SwaggerDoc");
    c.SwaggerEndpoint(swaggerDoc["Endpoint"], swaggerDoc["Title"]);
});
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

await app.RunAsync();