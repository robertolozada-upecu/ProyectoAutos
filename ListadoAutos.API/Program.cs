using ListadoAutos.API.Modelos;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("PermitirTodas", a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var pathDB = Path.Join("C:", "sqlite", "listaautos.db");
//var pathDB = Path.Join(Directory.GetCurrentDirectory(), "listaautos.db");
var conexion = new SqliteConnection($"Data Source={pathDB}");
builder.Services.AddDbContext<ListadoAutosDbContext>(o => o.UseSqlite(conexion));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("PermitirTodas");

app.MapGet("/autos", async (ListadoAutosDbContext db) => await db.Autos.ToListAsync());
app.MapGet("/autos/{id}", async (int id, ListadoAutosDbContext db) =>
    await db.Autos.FindAsync(id) is Auto auto ? Results.Ok(auto) : Results.NotFound()
);
app.MapPut("/autos/{id}", async (int id, Auto autoModificado, ListadoAutosDbContext db) =>
{
    var auto = await db.Autos.FindAsync(id);
    if(auto == null) return Results.NotFound();

    auto.Placa = autoModificado.Placa;
    auto.Marca = autoModificado.Marca;
    auto.Modelo = autoModificado.Modelo;
    await db.SaveChangesAsync();

    return Results.Ok();
});
app.MapDelete("/autos/{id}", async (int id, ListadoAutosDbContext db) =>
{
    var auto = await db.Autos.FindAsync(id);
    if (auto == null) return Results.NotFound();
    db.Remove(auto);
    await db.SaveChangesAsync();

    return Results.Ok();
});
app.MapPost("/autos", async (Auto auto, ListadoAutosDbContext db) =>
{
    await db.AddAsync(auto);
    await db.SaveChangesAsync();

    return Results.Created($"/autos/{auto.Id}", auto);
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
