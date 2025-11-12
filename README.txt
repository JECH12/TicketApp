Ticket API — Prueba Técnica Backend .NET + GraphQL

Proyecto desarrollado en C# (.NET 8) con GraphQL (HotChocolate) y SQL
Server, aplicando principios de Clean Architecture, Automapper, Entity
Framework Core, y unit testing con xUnit, Docker / Docker Compose.

------------------------------------------------------------------------

Objetivo

API que permite realizar operaciones CRUD sobre tickets de soporte,
implementando GraphQL como interfaz de consulta y ejecución de
operaciones, devolviendo respuestas consistentes mediante un wrapper
genérico Result<T>.

------------------------------------------------------------------------

Requisitos previos

-   .NET 8 SDK
-   Docker (para ejecutar SQL Server localmente)
-   Visual Studio 2022 o VS Code
-   Postman o Banana Cake Pop (para probar GraphQL)

------------------------------------------------------------------------

-- Ejecución en local (con Docker)

1.  Requisitos previos

   - Asegúrate de tener instalados:

   - Docker Desktop

   - Visual Studio 2022 con soporte para .NET 8

    -Git

2. Clonar el repositorio

    --git clone https://github.com/tu-usuario/TicketApp.git
    cd TicketApp	


3️. Verificar el archivo docker-compose.yml


El proyecto ya incluye un docker-compose.yml configurado con dos servicios:


version: '3.9'
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: sqlserver2022
    environment:
      SA_PASSWORD: "Esteban123"
      ACCEPT_EULA: "Y"
    ports:
      - "1433:1433"
    healthcheck:
      test: ["CMD-SHELL", "/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'Esteban123' -Q 'SELECT 1'"]
      interval: 10s
      retries: 10

  ticketapi:
    build:
      context: .
      dockerfile: Ticket.Application/Dockerfile
    container_name: ticket_api
    ports:
      - "5000:80"
    depends_on:
      sqlserver:
        condition: service_healthy

4. Levantar la solución

	Desde la raíz del proyecto: 

	docker-compose up --build

	Esto construira y ejecutara tanto la base de datos como el API 


5. Conexión de base de datos

	La API se conecta automáticamente a TicketDB (contenedor SQL) y aplica migraciones al iniciar.

	Cadena de conexión (en appsettings.json):

	"DefaultConnection": "Server=sqlserver,1433;Database=TicketDB;User Id=sa;Password=Esteban123;TrustServerCertificate=True;"
------------------------------------------------------------------------

Endpoints(GraphQL)

Queries

1. Obtener todos los tickets: 

query {
  tickets(first: 10, after: null) {
    totalCount
    nodes {
      id
      user
      status
      creationDate
      updateDate
    }
    pageInfo {
      hasNextPage
      hasPreviousPage
      endCursor
    }
  }
}

2. Obtener ticket por ID: 

query {
  ticketById(id: 1) {
    success
    message
    errors
    data {
      id
      user
      status
      creationDate
      updateDate
    }
  }
}

------------------------------------------------------------------------------
Mutations

1. Crear ticket: 

mutation {
  createTicket(input: { user: "Esteban", status: "Open" }) {
    success
    message
    errors
    data {
      id
      user
      status
      creationDate
      updateDate
    }
  }
}

2. Actualizar ticket: 

mutation {
  updateTicket(id: 1, input: {
    user: "Esteban Carrillo",
    status: "CLOSED"
  }) {
    success
    message
    errors
    data {
      id
      user
      status
      updateDate
    }
  }
}

3. Eliminar ticket: 
mutation {
  deleteTicket(id: 1) {
    success
    message
    errors
    data
  }
}
------------------------------------------------------------------------

 Pruebas Unitarias

Ejecutar todas las pruebas con: dotnet test

Se utilizan xUnit, FluentAssertions, AutoMapper, y EF Core InMemory.
Cubre todos los métodos del servicio: GetAll, GetByIdAsync, CreateAsync,
UpdateAsync, DeleteAsync.

------------------------------------------------------------------------

 Tecnologías utilizadas

-   C# / .NET 8
-   GraphQL (HotChocolate)
-   Entity Framework Core
-   SQL Server (Docker)
-   AutoMapper
-   xUnit + FluentAssertions
-   Docker

------------------------------------------------------------------------

-- Autor --

Esteban Carrillo
Desarrollador .NET / Angular

------------------------------------------------------------------------
