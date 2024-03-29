using CBRService.Services;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( o =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddSingleton<CurrencyService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<CurrencyService>(c => {
    c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("CbrData"));
});
builder.Services.Configure<RouteOptions>(o => {
    o.LowercaseUrls = true;
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();