using ServerApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<MainService>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();

public partial class Program{}
