using Rusty.Template.Infrastructure.Database;
using Rusty.Template.Infrastructure.Middlewares;
using Rusty.Template.Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
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
        "HTTP {RequestMethod} {RequestPath} ({UserId} responded {StatusCode} in {Elapsed:0.0000}ms)";
});
//Prepare db
app.Services.MigrateDatabase();
await app.Services.InitializeDatabaseDataAsync();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllerRoute(
    "default",
    "api/v1");
app.MapControllers();
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapDefaultControllerRoute();
//     endpoints.MapFallbackToFile("index.html");
// });
await app.RunAsync();