# 🏥 MedicalERP Backend

A robust and scalable Enterprise Resource Planning (ERP) backend system designed specifically for healthcare organizations, hospitals, clinics, laboratories, and medical centers. This application provides centralized management of patients, appointments, doctors, billing, inventory, prescriptions, and administrative operations through secure REST APIs.

---

## 📋 Overview

MedicalERP Backend serves as the core API layer for the MedicalERP ecosystem. It handles business logic, data management, authentication, authorization, reporting, and integration between different healthcare modules.

The system is built with scalability, security, and maintainability in mind, allowing healthcare institutions to streamline their daily operations while maintaining compliance with modern software development standards.

---

## ✨ Features

### Patient Management

* Patient registration and profile management
* Medical history tracking
* Patient search and filtering
* Emergency contact management
* Patient document management

### Doctor Management

* Doctor profile management
* Specialization management
* Schedule management
* Availability tracking
* Doctor-patient assignment

### Appointment Management

* Appointment booking
* Appointment rescheduling
* Appointment cancellation
* Status tracking
* Appointment history

### Prescription Management

* Prescription creation
* Medication management
* Treatment plans
* Prescription history
* Digital prescription records

### Billing & Invoicing

* Invoice generation
* Payment tracking
* Outstanding balance management
* Financial reporting
* Revenue analytics

### Inventory Management

* Medicine inventory tracking
* Stock monitoring
* Low stock alerts
* Supplier management
* Purchase records

### Authentication & Authorization

* JWT Authentication
* Role-Based Access Control (RBAC)
* Secure password hashing
* Refresh token support
* Protected API endpoints

### Reporting

* Patient reports
* Appointment reports
* Revenue reports
* Inventory reports
* Administrative dashboards

---

## 🏗️ System Architecture

The application follows a clean and maintainable layered architecture:

```text
├── API Layer
│   ├── Controllers
│   ├── Middleware
│   └── Filters
│
├── Application Layer
│   ├── Services
│   ├── DTOs
│   ├── Validators
│   └── Interfaces
│
├── Domain Layer
│   ├── Entities
│   ├── Enums
│   └── Business Rules
│
├── Infrastructure Layer
│   ├── Repositories
│   ├── Database Context
│   ├── External Services
│   └── File Storage
│
└── Database
    └── SQL Server
```

---

## 🛠️ Technology Stack

### Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core

### Database

* SQL Server

### Authentication

* JWT Bearer Authentication

### Documentation

* Swagger / OpenAPI

### Development Tools

* Visual Studio
* Git
* GitHub

---

## 📂 Project Structure

```text
MedicalERP.Backend
│
├── Controllers
├── Services
├── Repositories
├── Models
├── DTOs
├── Interfaces
├── Middleware
├── Validators
├── Migrations
├── Configurations
├── Helpers
├── Extensions
├── Common
└── Program.cs
```

---

## 🚀 Getting Started

### Prerequisites

Before running the project, make sure you have:

* .NET 8 SDK
* SQL Server
* Visual Studio 2022
* Git

---

### Clone Repository

```bash
git clone https://github.com/your-username/MedicalERP-Backend.git

cd MedicalERP-Backend
```

---

### Configure Database

Update your connection string in:

```json
appsettings.json
```

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=MedicalERP;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

### Apply Database Migrations

```bash
dotnet ef database update
```

---

### Run Application

```bash
dotnet run
```

The API will be available at:

```text
https://localhost:5001
```

or

```text
http://localhost:5000
```

---

## 📖 API Documentation

Swagger documentation is automatically available after running the application.

```text
https://localhost:5001/swagger
```

Use Swagger UI to:

* Test endpoints
* View request/response models
* Authenticate using JWT tokens
* Explore API contracts

---

## 🔐 Authentication

The application uses JWT authentication.

### Login Flow

1. User submits credentials.
2. Server validates user.
3. JWT token is generated.
4. Token is returned to client.
5. Client sends token in Authorization header.

Example:

```http
Authorization: Bearer YOUR_JWT_TOKEN
```

---

## 🧪 Testing

Run all tests:

```bash
dotnet test
```

Generate test coverage:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

---

## 📈 Future Enhancements

* Multi-Hospital Support
* Laboratory Management
* Radiology Module
* Pharmacy Integration
* Insurance Management
* Telemedicine Support
* SMS Notifications
* Email Notifications
* Mobile API Support
* AI-Based Analytics

---

## 🔒 Security Features

* JWT Authentication
* Password Hashing
* Role-Based Authorization
* Input Validation
* Exception Handling Middleware
* Secure API Endpoints
* SQL Injection Protection
* Cross-Origin Resource Sharing (CORS)

---

## 🤝 Contributing

Contributions are welcome.

1. Fork the repository.
2. Create a feature branch.

```bash
git checkout -b feature/new-feature
```

3. Commit changes.

```bash
git commit -m "Add new feature"
```

4. Push branch.

```bash
git push origin feature/new-feature
```

5. Create a Pull Request.

---

## 📝 License

This project is licensed under the MIT License.

---

## 👨‍💻 Author

**Ali Ahsan**

Full-Stack Developer

* ASP.NET Core
* C#
* Angular
* SQL Server
* Azure

---

## ⭐ Support

If you find this project useful, please consider giving it a star on GitHub.

```bash
⭐ Star the repository
🍴 Fork the project
🚀 Contribute improvements
```
