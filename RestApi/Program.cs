var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();


// Adicionar HttpClient para CountryService
builder.Services.AddHttpClient<ICountryService, CountryService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
