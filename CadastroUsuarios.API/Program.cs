using CadastroUsuarios.API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar o CORS para permitir o Front-end na porta 5174
builder.Services.AddCors(options => {
    options.AddPolicy("PermitirReact", policy => {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "http://localhost:5174", "https://localhost:5174")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

// 2. Configuração do PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// 3. Ativar o CORS antes de mapear os controllers
app.UseCors("PermitirReact");

app.UseAuthorization();

// 4. Mapear os controllers (DEVE vir antes de app.Run())
app.MapControllers();

app.Run();