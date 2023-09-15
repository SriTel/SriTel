using Microsoft.EntityFrameworkCore;
using SriTel.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<SriTelContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("SriTelDB"))
    );

builder.Services.AddDbContext<SriTelContext>(opt =>
    opt.UseInMemoryDatabase("SriTelDB"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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