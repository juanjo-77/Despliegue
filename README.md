Aquí va el README:

---

# AniEvents — Sistema de Gestión de Eventos Anime

Plataforma web para gestionar y visualizar eventos temáticos de anime. Desarrollada con ASP.NET Core MVC, MySQL y Materialize CSS.

## Tecnologías

- **Backend:** .NET 8, ASP.NET Core MVC
- **Base de datos:** MySQL + Entity Framework Core (Pomelo)
- **Frontend:** Materialize CSS (Material Design)
- **Despliegue:** Docker + VPS con Nginx + SSL (Let's Encrypt)

## Estructura del proyecto

```
AnimeEventsMVC/
├── Controllers/
│   └── EventsController.cs     # Lógica CRUD + galería
├── Models/
│   └── Event.cs                # Entidad con validaciones
├── Data/
│   └── AppDbContext.cs         # Contexto de Entity Framework
├── Views/
│   ├── Shared/_Layout.cshtml   # Layout con Materialize CSS
│   └── Events/
│       ├── Gallery.cshtml      # Galería pública con filtros
│       ├── Index.cshtml        # Panel de administración
│       ├── Create.cshtml       # Formulario de creación
│       ├── Edit.cshtml         # Formulario de edición
│       └── Delete.cshtml       # Confirmación de eliminación
└── Dockerfile                  # Imagen Docker
```

## Funcionalidades

- CRUD completo de eventos (Nombre, Fecha, Descripción, Ubicación, Categoría, Póster)
- Galería pública con grid de cards, filtros por categoría y orden por fecha
- Panel de administración con tabla y acciones de editar/eliminar
- Diseño responsivo con temática anime (Materialize CSS)
- Persistencia en MySQL en VPS compartido

## Instalación local

```bash
# Clonar el repositorio
git clone https://github.com/TU_USUARIO/AnimeEventsMVC.git
cd AnimeEventsMVC

# Configurar la cadena de conexión en appsettings.json
# "DefaultConnection": "Server=localhost;Port=3306;Database=anime_events_db;User=root;Password=TU_PASSWORD;"

# Aplicar migraciones
dotnet ef database update

# Correr el proyecto
dotnet run
```

## Despliegue con Docker

```bash
# Construir la imagen
docker build -t anime_events_app .

# Levantar el contenedor
docker run -d \
  --name anime_web \
  -p 8080:8080 \
  anime_events_app
```

El servidor usa Nginx como reverse proxy con SSL configurado via Let's Encrypt, apuntando el dominio al puerto 8080 del contenedor.

## Vistas

| Ruta | Descripción |
|---|---|
| `/Events/Gallery` | Galería pública de eventos |
| `/Events/Index` | Panel de administración |
| `/Events/Create` | Crear nuevo evento |
| `/Events/Edit/{id}` | Editar evento |
| `/Events/Delete/{id}` | Eliminar evento |

---
