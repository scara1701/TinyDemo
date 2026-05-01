using TinyDemo.SharedLib.Services;
using TinyDemo.WebAPI.Data;
using TinyDemo.WebAPI.Endpoints;
using TinyDemo.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers(); Retired MVC‑style controllers, now using minimal APIs with endpoint groups
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi(); // .NET 8+ native Minimal API Swagger
builder.Services.AddScoped<ILottoService,LottoService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    // 1. Start met een lege chain
    options.SerializerOptions.TypeInfoResolverChain.Clear();

    // 2. Voeg eerst je logging resolver toe
    options.SerializerOptions.TypeInfoResolverChain.Add(new JsonLoggingResolver(MyJsonContext.Default));

    // 3. Voeg daarna je eigen context toe
    options.SerializerOptions.TypeInfoResolverChain.Add(MyJsonContext.Default);
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // .NET 8+ native Minimal API Swagger
}

app.UseHttpsRedirection();


app.MapGet("/__startup_test", () => "OK");
app.MapLottoEndpoints();

app.Run();
