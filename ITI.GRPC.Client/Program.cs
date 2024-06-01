var builder = WebApplication.CreateBuilder(args);

// Register MVC services
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
// app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
