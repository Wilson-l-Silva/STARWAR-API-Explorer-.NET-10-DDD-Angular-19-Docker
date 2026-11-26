using SwapiExplorer.Application;
using SwapiExplorer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- Registro de serviços (Dependency Injection) ---

// Controllers com suporte a JSON (padrão)
builder.Services.AddControllers();

// Swagger/OpenAPI: gera documentação interativa da API em /swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cada camada expõe seu próprio método de extensão — mantém o Program.cs enxuto e legível.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// CORS: necessário para o Angular (rodando em outra porta/origem) conseguir chamar esta API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // porta padrão do Angular em dev
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// --- Pipeline HTTP (middlewares, em ordem de execução) ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // acessível em https://localhost:{porta}/swagger
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp"); // precisa vir antes de MapControllers

app.UseAuthorization();

app.MapControllers();

app.Run();