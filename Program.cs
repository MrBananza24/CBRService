using CBRService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<CurrencyService>();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<CurrencyService>(c => {
    c.BaseAddress = new Uri(builder.Configuration.GetConnectionString("CbrData"));
});
builder.Services.Configure<RouteOptions>(options => {
    options.LowercaseUrls = true;
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