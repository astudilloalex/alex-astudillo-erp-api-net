# A ERP startup project
[A ERP API](https://www.postman.com/alex-astudillo/workspace/public-workspace/collection/15834347-80f18b59-429d-47dd-bdf1-4102715bd777?action=share&creator=15834347)

## Requirements
* This project works with NET 7.
* You need a PostgreSQL database for run project, in this case I use the 11.19 version, on Ubuntu Server.

### Required packages
#### Domain Project
* Microsoft.AspNetCore.Authentication.JwtBearer (7.0.5)
* Microsoft.EntityFrameworkCore (7.0.5)
* NLog (5.1.4)
* Npgsql.EntityFrameworkCore.PostgreSQL (7.0.4)
#### API Project
* Microsoft.AspNetCore.OpenApi (7.0.5).
* Swashbuckle.AspNetCore (6.4.0)

## Architecture
This project works with Clean Architecture (Hexagonal Architecture).

### Domain
This is the core of the project, you can see:
* **Entities**: The class representation of database table row, separated by schemas.
* **Enums**: Value type that represents a set of named constants, separated by schemas and custom enums.
* **Exceptions**: A custom exceptions to throw in the system.
* **Interfaces**: Defines a contract or a set of method signatures, properties, and events that a class must implement; separated by services (schemas and custom) and repositories (schemas).

### Application
Contains the business logic.
* **Services**: You can see the all implemented services declared in the domain.

### Infrastructure
The infrastructure layer is responsible for providing implementation details and mechanisms for interacting with external resources and technologies, such as databases, file systems, web services, frameworks, and other infrastructure components.
* **Repositories**: Components responsible for interacting with databases or other data storage systems. This may include implementations of repositories, data mappers, or ORM frameworks.
* **Connections**: Declare all database connections.

### API
Contains all API endpoints to send data to clients.


#### Note
This can change in the future, check regularly.