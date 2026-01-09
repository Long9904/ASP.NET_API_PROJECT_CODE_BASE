
# 🚀 ASP.NET Core API Base Project

A clean, scalable, and production-oriented base project for building RESTful APIs with **ASP.NET Core (.NET 8)**.

> **Note:** This project is currently in the early stages of development.  
> The goal is to provide a solid foundation following real-world backend practices  
> such as Clean Architecture, CQRS, structured error handling, and observability.

---

## 🏗️ Technologies & Architecture

### Architecture

- **Clean Architecture**
  - Presentation (API)
  - Application
  - Domain
  - Infrastructure

### Framework

- **.NET 8.0**

### Database

- **PostgreSQL**
- **Entity Framework Core**

---

## 📦 Libraries & Design Patterns

### Application Layer

- **MediatR**
  - CQRS-style Commands & Queries
  - Decouples controllers from business logic

- **Result Pattern**
  - Explicit success/failure handling
  - Business errors are not thrown as exceptions
  - Errors are mapped to HTTP responses at the API layer

- **FluentValidation**
  - Strongly-typed request validation
  - Integrated via MediatR pipeline behavior

---

## 🧩 Cross-Cutting Concerns

### MediatR Pipeline Behaviors

- **ValidationBehavior**
  - Automatically validates requests using FluentValidation
  - Throws validation exceptions for invalid requests

---

## ⚠️ Error Handling Strategy

### Global Exception Handling

- Custom **ExceptionHandlingMiddleware**
- Handles:
  - Validation exceptions (400)
  - Unhandled system exceptions (500)

### Problem Details (RFC 7807)

The API follows the **Problem Details** specification for error responses.

#### Example: Business Error (409)

```json
{
  "title": "Email already exists",
  "status": 409,
  "errorCode": "USER_EMAIL_EXISTS",
  "traceId": "0HMV3A1H9FJ23"
}
````

#### Example: Validation Error (400)

```json
{
  "title": "Validation failed",
  "status": 400,
  "errors": {
    "email": ["Invalid email format"]
  }
}
```

#### Example: System Error (500)

```json
{
  "title": "Internal Server Error",
  "status": 500,
  "errorCode": "INTERNAL_SERVER_ERROR",
  "traceId": "0HMV3A1H9FJ23"
}
```

---

## 🔍 Observability

### TraceId

* Every HTTP request is assigned a unique **TraceId**
* TraceId is:

  * Returned in error responses
  * Logged on the server side
* Enables fast debugging and log correlation in production

---

## 📄 API Response Conventions

### Success Responses

* Return **data only**
* No envelope (`success`, `data`, etc.)
* HTTP status code represents success

```json
{
  "id": 1,
  "username": "john",
  "email": "john@example.com"
}
```

### Error Responses

* Follow Problem Details format
* Include metadata for debugging and client-side handling

---

## 📚 Feature Organization

Features are organized by **use case**, not by technical layers.

Example:

```
Users
 ├── CreateUser.cs
 ├── UpdateUser.cs
 ├── DeleteUser.cs
 ├── GetUserById.cs
 └── UserDto.cs
```

* Simple features may be grouped
* Complex features (e.g. Auth) can be split into subfolders

---

## 🛠️ Prerequisites

| Component        | Requirement                |
| ---------------- | -------------------------- |
| 💻 **IDE**       | Visual Studio 2022 (17.8+) |
| 📦 **SDK**       | .NET 8 SDK                 |
| 🗄️ **Database** | PostgreSQL                 |

---

## 📋 Version History

### V1 – 31/12/2025

**Database Connection & Migration Setup**

#### Setup Instructions

1. **Create Configuration File**

   * Create: `appsettings.Development.json`
   * Location: `NET_8.0_Projects`

2. **Add Connection String**

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=YourDBSchemaName;Username=postgres;Password=YourPassword"
     }
   }
   ```

3. **Run Database Migration**

   * Open **Package Manager Console**
   * Select default project: `Infrastructure`
   * Run:

     ```
     Update-Database
     ```

---

## 🎯 Project Goals

* Provide a real-world **API starter template**
* Emphasize:

  * Clean Architecture
  * Explicit error handling
  * Production-ready patterns
    
* Suitable for:
  * Graduation projects
  * Learning backend best practices

---

## 📌 Roadmap (Planned)

* Authentication & Authorization
* Pagination & filtering helpers
* Centralized logging (Serilog)
* API versioning

---

## 🤝 Contributions

Contributions, feedback, and suggestions are welcome.

```

