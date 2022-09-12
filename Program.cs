using Microsoft.EntityFrameworkCore;
using ModuloPortaria.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ModuloPortaria.Data.AppCont>(options =>
{
    options.UseSqlite(builder
        .Configuration
        .GetConnectionString("DbVisitas"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

#region API

app.MapGet("/visitante", async (ModuloPortaria.Data.AppCont DbContext) => await DbContext.visitantes.ToListAsync());

app.MapGet("/visitante/{id}", async (int id, ModuloPortaria.Data.AppCont DbContext) => 
    await DbContext.visitantes.FirstOrDefaultAsync(a => a.Id == id));

app.MapPost("/visitante", async (visitante visitante, ModuloPortaria.Data.AppCont DbContext) =>
{
    DbContext.visitantes.Add(visitante);
    await DbContext.SaveChangesAsync();

    return visitante;
});

app.MapPut("/visitante/{id}", async (int id, visitante visitante, ModuloPortaria.Data.AppCont DbContext) =>
{
    DbContext.Entry(visitante).State = EntityState.Modified;
    await DbContext.SaveChangesAsync();
    return visitante;
});

app.MapDelete("/visitante/{id}", async (int id, ModuloPortaria.Data.AppCont DbContext) =>
{
    var visitante = await DbContext.visitantes.FirstOrDefaultAsync(a => a.Id == id);

    DbContext.visitantes.Remove(visitante);
    await DbContext.SaveChangesAsync();
    return;
});

#endregion

app.Run();
