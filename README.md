# MedicalERP Backend

MedicalERP Backend is a healthcare management system built with ASP.NET Core Web API and Clean Architecture principles. It provides a secure, scalable, and maintainable foundation for managing patients, appointments, prescriptions, billing, inventory, and administrative operations within hospitals, clinics, and medical organizations.

## Features

The system includes patient and appointment management, prescription handling, billing and invoicing, medicine inventory tracking, reporting, and role-based access control. Authentication is implemented using JWT, ensuring secure access to protected resources and administrative functions.

## Architecture

The project follows a layered architecture to promote separation of concerns and maintainability.

```text
MedicalERP
├── API
│   ├── Controllers
│   ├── Middleware
│   └── Filters
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   └── Validators
│
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Common
│
├── Infrastructure
│   ├── Data
│   ├── Services
│   └── External Integrations
│
└── Tests
```

## Technology Stack

* ASP.NET Core 8 Web API
* C#
* Entity Framework Core
* SQL Server
* JWT Authentication
* Swagger / OpenAPI
* xUnit

## Getting Started

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio 2022 or VS Code
* Git

### Installation

Clone the repository:

```bash
git clone https://github.com/your-username/MedicalERP.git
cd MedicalERP
```

Configure the database connection in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MedicalERP;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Apply migrations:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

The API will be available locally at:

```text
https://localhost:7165
```

## API Documentation

Swagger is enabled by default and can be accessed after starting the application:

```text
https://localhost:7165/swagger
```

The documentation provides endpoint details, request/response schemas, and JWT authentication support for testing secured APIs.

## Testing

Run the test suite with:

```bash
dotnet test
```

## Security

MedicalERP implements JWT authentication, role-based authorization, password hashing, request validation, centralized exception handling, and secure API practices to help protect application data and resources.

## Contributing

Contributions are welcome. Feel free to submit issues, suggest improvements, or create pull requests.

## License

This project is licensed under the MIT License.

## Author

Ali Ahsan

Full-Stack Developer specializing in ASP.NET Core, Angular, SQL Server, and modern web application development.
