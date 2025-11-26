// Program.cs
using UserManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DEL SERVICIO ---
builder.Services.AddControllers();
// Inyección de Dependencias: Registra el servicio de usuario
builder.Services.AddSingleton<IUserService, UserService>(); 
// Configuración de CORS para permitir solicitudes del frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// --- 2. MIDDLEWARE PIPELINE ---

// Middleware: Gestión de Entorno (Swagger/OpenAPI solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger(); // Opcional, para documentación de API
    // app.UseSwaggerUI();
}

// Middleware: Redirección HTTPS (buena práctica)
app.UseHttpsRedirection();

// Middleware: Enrutamiento
app.UseRouting();

// Middleware: CORS (Aplicado globalmente)
app.UseCors("AllowAll");

// Middleware: Autorización (si se implementara seguridad)
// app.UseAuthorization(); 

// Middleware: Mapeo de Controladores
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();