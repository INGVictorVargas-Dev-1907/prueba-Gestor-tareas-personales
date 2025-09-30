# 📌 TaskManager API  
Backend para la gestión de tareas personales, desarrollado con **.NET 9 Web API** y **SQL Server**.  

Permite operaciones CRUD básicas sobre tareas:  
- 📄 Listar todas las tareas  
- ➕ Crear nuevas tareas  
- ✔ Marcar tareas como completadas  
- ❌ Eliminar tareas  

---

## ⚙️ Tecnologías usadas
- **.NET 9 Web API (C#)**
- **Entity Framework Core 9 (Database First con SQL Server)**
- **SQL Server**
- **Swagger / OpenAPI**
- **Visual Studio 2022**

---

## 📂 Estructura del proyecto
```bash
TaskManager.Api/
│── Controllers/
│ └── TasksController.cs
│── Data/
│ └── TaskDbContext.cs
│── Models/
│ ├── TaskItem.cs
│ └── ApiResponse.cs
│── Program.cs
│── appsettings.json

```

---

## 🛠 Configuración inicial

### 1️⃣ Clonar el repositorio
```bash
git clone https://github.com/tuusuario/prueba-gestor-tareas-personales.git
cd prueba-gestor-tareas-personales

```

---

## Configurar la base de datos en SQL Server
Ejecuta en SQL Server Management Studio (SSMS):
```sql
CREATE DATABASE TaskManagerDb;
GO

USE TaskManagerDb;
GO

CREATE TABLE Tasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE()
);
```

---

## Configurar la cadena de conexión
En el archivo appsettings.json:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=[servidor];Database=[nobre de la base de datos];Trusted_Connection=True;TrustServerCertificate=True;"
}
```

--- 

## 🚀 Endpoints disponibles

La API expone los siguientes endpoints:

| Método | Endpoint                   | Descripción                      |
|--------|----------------------------|----------------------------------|
| GET    | `/api/tasks`               | Listar todas las tareas          |
| GET    | `/api/tasks/{id}`          | Obtener una tarea por ID         |
| POST   | `/api/tasks/guardar`       | Crear una nueva tarea            |
| PUT    | `/api/tasks/{id}/complete` | Marcar una tarea como completada |
| DELETE | `/api/tasks/{id}`          | Eliminar una tarea               |

--

## 🔧 Scaffold de la base de datos

Si generas los modelos a partir de la base de datos (**Database First**), usa el siguiente comando en la **Consola del Administrador de Paquetes (PMC)**:

```powershell
Scaffold-DbContext "Server=localhost;Database=TaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context TaskDbContext -ContextDir Data
```

---

## ejemplo de respuesta
```json
{
  "success": true,
  "message": "Tareas obtenidas correctamente",
  "data": [
    {
      "id": 1,
      "title": "Comprar leche",
      "isCompleted": true,
      "createdAt": "2025-09-30T17:45:00Z"
    }
  ]
}
```

## Documentacion swagger (recuerda ejecutarlo si usas visual studio 2022 iiS Express)
```url
https://localhost:44331/swagger
```

---

## Autor

Victor Alfonso Vargas Diaz

📌 Proyecto creado como prueba técnica con .NET 9 + SQL Server.
📌 Licencia: MIT





