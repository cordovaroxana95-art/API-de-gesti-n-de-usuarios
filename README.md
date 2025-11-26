🚀 API de Gestión de Usuarios (ASP.NET Core / C#)

Este proyecto implementa una API Web robusta para la gestión de usuarios, desarrollada utilizando ASP.NET Core Web API en C#. La arquitectura se basa en el principio de separación de preocupaciones, utilizando Controladores, una capa de Servicios (para la lógica de negocio) y DTOs (Data Transfer Objects) para la entrada y salida de datos, incluyendo la validación.

✨ Características de la API
Característica,Implementación,Detalle
CRUD Completo,UsersController,"Puntos finales RESTful para Crear, Leer, Actualizar y Eliminar usuarios."
Validación Robusta,Anotaciones de Datos y DTOs,"Usa las anotaciones estándar de C# ([Required], [EmailAddress], etc.) para garantizar la integridad de los datos de entrada."
Lógica de Negocio,UserService,Implementa la validación de unicidad del correo electrónico separada de la capa de presentación.
Modularidad,Servicios e Interfaces,"Uso de Inyección de Dependencias (DI) para la capa de servicio (IUserService), facilitando las pruebas unitarias y el mantenimiento."
Middleware,Program.cs,Configuración de CORS para permitir el acceso desde aplicaciones de frontend externas.

🗂️ Estructura del Proyecto

La organización del código sigue las convenciones de ASP.NET Core:

/UserManagementApi
|-- **Controllers/** <-- Maneja las peticiones HTTP y define los endpoints (UsersController.cs).
|-- **Data/** <-- Simulación de la base de datos (InMemoryContext.cs).
|-- **DTOs/** <-- Objetos de transferencia de datos utilizados para validar y transferir información (UserCreateDto.cs).
|-- **Models/** <-- Clases de dominio (User.cs).
|-- **Services/** <-- Lógica de negocio y validación de unicidad (IUserService.cs, UserService.cs).
|-- Program.cs              <-- Punto de entrada, configuración de Middleware y DI.

🛠️ Configuración y Ejecución

1. Requisitos Previos

.NET SDK (versión 6.0 o superior recomendada).
Un IDE como Visual Studio o Visual Studio Code con las extensiones de C#.

2. Pasos de Ejecución

Clona o descarga el proyecto y navega al directorio raíz:
cd UserManagementApi

Restaura las dependencias (NuGet packages):
dotnet restore

Ejecuta la aplicación:
dotnet run

La API se iniciará, típicamente en http://localhost:5000 o http://localhost:5001 (HTTPS), según la configuración de tu entorno.

🌐 Puntos Finales de la API (Endpoints)

Todos los puntos finales están prefijados por /api/users.

Método,Ruta,Descripción,Código de Respuesta
GET,/api/users,Obtiene la lista completa de usuarios.,200 OK
GET,/api/users/{id},Obtiene un usuario por ID.,"200 OK, 404 Not Found"
POST,/api/users,Crea un nuevo usuario.,"201 Created, 400 Bad Request, 409 Conflict"
PUT,/api/users/{id},Actualiza un usuario existente.,"200 OK, 400 Bad Request, 404 Not Found, 409 Conflict"
DELETE,/api/users/{id},Elimina un usuario por ID.,"204 No Content, 404 Not Found"

🚨 Manejo de Errores y Validación

400 Bad Request (Error de Validación Básica): Devuelto automáticamente por el framework cuando los datos del DTO no cumplen con las anotaciones (e.g., email inválido, campos requeridos faltantes). La respuesta contendrá los detalles del ModelState.

409 Conflict (Error de Lógica de Negocio): Devuelto por el servicio cuando se intenta crear o actualizar un usuario con un correo electrónico que ya existe en el sistema.

Ejemplo de Solicitud (POST)

POST /api/users HTTP/1.1
Host: localhost:5000
Content-Type: application/json

{
    "name": "Diana Prince",
    "email": "diana.prince@dc.com",
    "age": 30
}

Respuesta Exitosa (201 Created):
{
    "id": 3,
    "name": "Diana Prince",
    "email": "diana.prince@dc.com"
}

Ejemplo de Respuesta de Error de Validación (400)
POST /api/users HTTP/1.1

{ "name": "Test", "email": "malformed.email" }

Respuesta (400 Bad Request):
{
    "errors": {
        "Email": [
            "El email debe tener un formato válido."
        ],
        "Age": [
            "La edad debe ser un número entre 18 y 150."
        ]
    },
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    // ... otros detalles ...
}
